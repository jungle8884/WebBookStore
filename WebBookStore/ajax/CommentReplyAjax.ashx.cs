using com.Utility;
using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebBookStore.ajax
{
    /// <summary>
    /// CommentReplyAjax 的摘要说明
    /// </summary>
    public class CommentReplyAjax : IHttpHandler
    {
        string json = "";
        HttpContext context;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Request.ContentEncoding = Encoding.GetEncoding("utf-8");
            string cmd = context.Request.QueryString["cmd"];
            switch (cmd)
            {
                case "add":
                    json = AddReplyComment();
                    break;
            }
            context.Response.Write(json);
        }

        /// <summary>
        /// 回复当前用户的评论
        /// </summary>
        /// <returns></returns>
        public string AddReplyComment()
        {
            int webCommentId = Convert.ToInt32(context.Request.Form["webCommentId"].ToString());
            string text = context.Request.Form["text"].ToString();
            if (CRegex.FilterHTML(text) == "")
            {
                rm.Info = "内容不能为空";
                return jss.Serialize(rm);
            }
            else if (CRegex.FilterHTML(text).Length > 500 || CRegex.FilterHTML(text).Length < 6)
            {
                rm.Info = "回复内容长度在6~500之间";
                return jss.Serialize(rm);
            }
            else
            {
                string strIP = WebHelp.GetIP();
                User user = UserDal.CurrentUser();//获取当前登陆用户
                List<dbParam> list = new List<dbParam>()
                {
                    new dbParam() { ParamName = "@ClientIP", ParamValue = strIP },
                    new dbParam() { ParamName = "@UserId", ParamValue =  user.UserId}
                };
                #region 同一IP,同一当前日期（年月日）,可以确定当天回复次数。
                List<WebCommentReply> wcrList = CommentReplyDAL.m_WebCommentReplyDAL.GetList(" ClientIP=@ClientIP and UserId=@UserId", list);
                int count = 0;
                if (wcrList.Count == 0)
                {
                    count = 0;
                }
                else
                {
                    string DateCurrent = string.Format("{0:D}", DateTime.Now);//设置当前日期（年-月-日）
                    foreach (var wcr in wcrList)
                    {
                        if (DateCurrent == string.Format("{0:D}", wcr.CreatedTime))
                            count++;
                    }
                }
                #endregion
                //同一用户不能一天超过三次留言
                if (count >= 3)
                {
                    rm.Info = "一天最多只能回复三次";
                    jss.Serialize(rm);
                }
                else
                {
                    if (user.Type < 0)
                    {
                        rm.Info = "只有已登录用户用户才能评论";
                        jss.Serialize(rm);
                    }
                    else
                    {
                        WebCommentReply webComReply = new WebCommentReply();
                        webComReply.WebCommentId = webCommentId;
                        webComReply.UserId = user.UserId;
                        webComReply.CommentReplyText = text;
                        webComReply.CreatedTime = DateTime.Now;
                        webComReply.ClientIP = WebHelp.GetIP();
                        CommentReplyDAL.m_WebCommentReplyDAL.Add(webComReply);

                        rm.Success = true;
                        rm.Info = "评论成功";
                    }
                }
            }

            return jss.Serialize(rm);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
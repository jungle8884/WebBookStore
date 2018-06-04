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
    /// CommentAjax 的摘要说明
    /// </summary>
    public class CommentAjax : IHttpHandler
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
                    json = AddComment();
                    break;
            }
            context.Response.Write(json);
        }

        public string AddComment()
        {
            string title = context.Request.Form["title"].ToString();
            string text = context.Request.Form["text"].ToString();
            //过滤html标签再判断是否为空
            if (CRegex.FilterHTML(text) == "")
            {
                rm.Info = "内容不能为空";
                return jss.Serialize(rm);
            }
            else if (CRegex.FilterHTML(text).Length > 500 || CRegex.FilterHTML(text).Length < 6)
            {
                rm.Info = "问题内容长度在6~500之间";
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
                #region 同一IP,同一当前日期（年月日）,可以确定当天评论次数。
                List<WebComment> wList = WebCommentDAL.m_WebCommentDal.GetList(" ClientIP=@ClientIP and UserId=@UserId", list);
                int count = 0;
                if (wList.Count == 0)
                {
                    count = 0;
                }
                else
                {
                    string DateCurrent = string.Format("{0:D}", DateTime.Now);//设置当前日期（年-月-日）
                    foreach (var w in wList)
                    {
                        if (DateCurrent == string.Format("{0:D}", w.CreatedTime))
                            count++;
                    }
                }
                #endregion
                //同一用户不能一天超过三次留言
                if (count >= 3)
                {
                    rm.Info = "一天最多只能发帖三次";
                    jss.Serialize(rm);
                }
                else
                {
                    if (user.Type < 0)
                    {
                        rm.Info = "只有已登录用户才能发帖";
                        jss.Serialize(rm);
                    }
                    else
                    {
                        WebComment webCom = new WebComment();
                        webCom.CommentTitle = title;
                        webCom.CommentText = text;
                        webCom.CreatedTime = DateTime.Now;
                        webCom.ClientIP = WebHelp.GetIP();
                        webCom.UserId = user.UserId;
                        WebCommentDAL.m_WebCommentDal.Add(webCom);

                        rm.Success = true;
                        rm.Info = "提交成功";
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
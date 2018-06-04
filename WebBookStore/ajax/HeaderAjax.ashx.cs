using Model;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using com.Utility;

namespace WebBookStore.ajax
{
    /// <summary>
    /// HeaderAjax 的摘要说明
    /// </summary>
    public class HeaderAjax : IHttpHandler, IRequiresSessionState
    {
        //全局变量
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
                case "checkislogin":
                    json = CheckIsLogin();
                    break;
                case "userlogin":
                    json = UserLogin();
                    break;
            }
            context.Response.Write(json);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public string UserLogin()
        {
            string username = context.Request.Form["username"].ToString();
            string pwd = context.Request.Form["pwd"].ToString();
            try
            {
                List<dbParam> list = new List<dbParam>() { new dbParam() { ParamName = "@UserName", ParamValue = username },
                new dbParam() { ParamName = "@Pwd", ParamValue = pwd }};
                User user = UserDal.m_UserDal.GetModel("UserName=@UserName and Pwd=@Pwd", list);
                //保存UserId，为后面的页面使用；比如购物车页面。
                //HttpContext.Current.Session["UserId"] = user.UserId;
                cookieHelper.SetCookie("UserId", user.UserId.ToString(), 3600);
                if (user != null)
                {
                    //存储登录者的 ip/用户id/密码 并加密
                    cookieHelper.SetCookie("CLoginUser", cookieHelper.EncryptCookie(string.Format("{0}/{1}/{2}", WebHelp.GetIP(), user.UserId, user.Pwd)), 60);
                    rm.Success = true;
                }
                else
                {
                    rm.Info = "用户名或密码错误";
                }
            }
            catch
            {
                rm.Info = "未知错误";
            }

            return jss.Serialize(rm);
        }

        /// <summary>
        /// 检查用户是否登录
        /// </summary>
        /// <returns></returns>
        public string CheckIsLogin()
        {
            if (UserDal.CurrentUser() != null)
            {
                rm.Success = true;
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
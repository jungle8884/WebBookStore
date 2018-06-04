using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using Model;
using Dal;
using com.Utility;
using System.Web.SessionState;

namespace WebBookStore.ajax
{
    /// <summary>
    /// RegAjax 的摘要说明
    /// </summary>
    public class RegAjax : IHttpHandler, IRequiresSessionState
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
                case "checkusername":
                    json = CheckUserName();
                    break;
                case "reguser":
                    json = RegUser();
                    break;
            }
            context.Response.Write(json);
        }

        /// <summary>
        /// 验证用户名是否已存在
        /// </summary>
        /// <returns></returns>
        public string CheckUserName()
        {
            string username = context.Request.Form["username"].ToString();
            rm.Success = true;
            try
            {
                List<dbParam> list = new List<dbParam>() {
                    new dbParam(){ ParamName = "@UserName", ParamValue = username}
                };
                User user = UserDal.m_UserDal.GetModel("UserName=@UserName", list);
                if(user != null)
                {
                    rm.Success = false;
                    rm.Info = "该用户已经注册";
                }
            }
            catch (Exception)
            {
                rm.Success = false;
                rm.Info = "未知错误";
            }
            return jss.Serialize(rm);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public string RegUser()
        {
            string username = context.Request.Form["username"].ToString();
            string pwd = context.Request.Form["pwd"].ToString();
            string email = context.Request.Form["email"].ToString();
            string phonenum = context.Request.Form["phonenum"].ToString();
            string qq = context.Request.Form["qq"].ToString();
            string checkcode = context.Request.Form["checkcode"].ToString();
            if (checkcode != context.Session["CheckCode"].ToString())//Session["CheckCode"] = rand;在image.aspx页面设置
            {
                rm.Success = false;
                rm.Info = "验证码输入不正确";
            }
            else
            {
                try
                {
                    User user = new User();
                    user.UserName = username;
                    user.Pwd = pwd;
                    user.Gender = "男";//默认为男，可在个人中心更改
                    user.Email = email;
                    user.Tel = phonenum;
                    user.QQ = qq;
                    user.Type = 0;//1管理员 0普通用户
                    user.ClientIP = WebHelp.GetIP();//获取到访问者的IP
                    user.CreatedTime = DateTime.Now;
                    user.HeadPic = "man.GIF";
                    if (!UserDal.RegLimit())
                    {
                        rm.Info = "sorry,一天最多只能注册三次";
                    }
                    else
                    {
                        int userid = UserDal.m_UserDal.Add(user);
                        //存储注册者的 ip/用户id/密码 并加密
                        cookieHelper.SetCookie("CLoginUser", cookieHelper.EncryptCookie(string.Format("{0}/{1}/{2}", WebHelp.GetIP(), userid, pwd)), 20);
                        rm.Success = true;
                        rm.Info = "恭喜您，注册成功，3秒后返回首页...";
                        
                    }
                }
                catch
                {
                    rm.Info = "未知错误";
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
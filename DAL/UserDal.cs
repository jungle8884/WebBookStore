using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.Utility;

namespace Dal
{
    /// <summary>
    /// 对用户的数据访问
    /// </summary>
    public class UserDal 
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<User> m_UserDal = new BaseDAL<User>();

        /// <summary>
        /// 注册限制
        /// </summary>
        /// <returns></returns>
        public static bool RegLimit()
        {
            bool b = true;
            List<dbParam> list = new List<dbParam>()
            {
                new dbParam() { ParamName = "@ClientIP", ParamValue = WebHelp.GetIP() },
            };
            #region 同一IP,同一当前日期（年月日）,可以确定当天注册次数。
            List<User> uList = UserDal.m_UserDal.GetList(" ClientIP=@ClientIP", list);
            int count = 0;
            string DateCurrent = string.Format("{0:D}", DateTime.Now);//设置当前日期（年-月-日）
            foreach (var u in uList)
            {
                if (DateCurrent == string.Format("{0:D}", u.CreatedTime))
                    count++;
            }
            #endregion
            if (count >= 3)
            {
                b = false;
            }
            return b;
        }
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static User CurrentUser()
        {
            User user = null;
            if (System.Web.HttpContext.Current.Request.Cookies["CLoginUser"] == null || System.Web.HttpContext.Current.Request.Cookies["CLoginUser"].Value == "")
            {
                return user;
            }
            else
            {
                //获取当前用户的Cookies并解密
                string strLoginUser = cookieHelper.DecryptCookie(System.Web.HttpContext.Current.Request.Cookies["CLoginUser"].Value);
                string[] aLoginUser = strLoginUser.Split('/');
                if (aLoginUser.Length != 3)
                {
                    user = null;
                }
                if (WebHelp.GetIP() != aLoginUser[0])
                {
                    user = null;
                }
                else
                {
                    user = UserDal.m_UserDal.GetModel("UserId=@UserId", new List<dbParam>() { new dbParam() { ParamName = "@UserId", ParamValue = Convert.ToInt32(Convert.ToInt32(aLoginUser[1])) } });
                    if (user.Pwd != aLoginUser[2])
                    {
                        user = null;
                    }
                }
            }
            return user;
        }
        /// <summary>
        /// 退出
        /// </summary>
        public static void LogOut()
        {
            cookieHelper.SetCookie("CLoginUser", "", -1);
        }
    }
}

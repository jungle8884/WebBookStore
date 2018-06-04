using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using com.Utility;

namespace WebBookStore.aspx
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserDal.CurrentUser() == null)
            {
                Response.Redirect("/aspx/index.aspx");
            }
        }

        private int _bookId;
        /// <summary>
        /// 图书的ID
        /// </summary>
        public int BookId
        {
            get
            {
                try
                {
                    _bookId = int.Parse(Request.QueryString["bookId"]);
                }
                catch (Exception)
                {
                    //表示没有获取到此图书ID
                    _bookId = -1;
                    return -1;
                }
                return _bookId;
            }
            set
            {
                _bookId = value;
            }
        }

        private int _page;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page
        {
            get
            {
                try
                {
                    _page = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"].ToString());
                }
                catch
                {
                    _page = 1;
                }
                return _page;
            }
            set { _page = value; }
        }

        /// <summary>
        /// 返回购物车页面代码
        /// 登录之后才会有页面，这一点一定要记住
        /// </summary>
        /// <returns></returns>
        public string GetShoppingCartHtmls()
        {
            int UserId = int.Parse(cookieHelper.GetCookie("UserId"));//若有，一定大于0
            if(UserId > 0)
            {
                return ShoppingCartDAL.GetShoppingCartHtmls(UserId, page);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获得分页控件
        /// </summary>
        /// <returns></returns>
        public string GetPagerDivided()
        {
            return PagerDal.GetPagerHtml(page, ShoppingCartDAL.ShoppingCartShowSize, ShoppingCartDAL.ShoppingCartSize);
        }
    }
}
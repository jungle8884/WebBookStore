using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class Board : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 留言网页
        /// </summary>
        /// <returns></returns>
        public string GetWebCommentHtmls()
        {
            return WebCommentDAL.GetWebCommentHtmls(page);
        }

        /// <summary>
        /// 获得分页控件
        /// </summary>
        /// <returns></returns>
        public string GetPagerDivided()
        {
            return PagerDal.GetPagerHtml(page, WebCommentDAL.pageShowSize, WebCommentDAL.CommentCount());
        }

    }
}
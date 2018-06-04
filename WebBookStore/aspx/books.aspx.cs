using Model;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["firstToSee"] == null)
            {
                Response.Redirect("/aspx/index.aspx");
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
        /// 获得指定数量的书籍
        /// </summary>
        /// <returns></returns>
        public string GetBooksHtml()
        {
            string searchWhat = null;
            if (Request.Form["search_books"] != null)
            {
                searchWhat = Request.Form["search_books"].ToString();
                BookDal.strWhere = string.Format("BookName like '%{0}%' or BookTitle like '%{0}%' or BookClassfication like '%{0}%'", searchWhat);
            }
            else
            {
                BookDal.strWhere = string.Format("1=1");
            }
            //查询数据
            if (string.IsNullOrEmpty(searchWhat))
            {
                return BookDal.GetBooksHtml(page);
            }
            else
            {

                return BookDal.GetBooksHtml(page);
            }
        }

        /// <summary>
        /// 获得分页控件
        /// </summary>
        /// <returns></returns>
        public string GetPagerDivided()
        {
            return PagerDal.GetPagerHtml(page, BookDal.pageShowSize, BookDal.bookSize());
        }

        /// <summary>
        /// 公告栏信息
        /// </summary>
        /// <returns></returns>
        public string GetInformBookHTML()
        {
            StringBuilder sb = new StringBuilder();
            //获取可以看到的且是书籍信息
            List<Inform> InfoList = InformDAL.m_InformDal.GetList(string.Format(" IsVisible=1 and InfoType=0 "));
            foreach (var Info in InfoList)
            {
                string sInformText = Info.InformText;
                sb.Append(string.Format("<p><a>{0}</a></p>", sInformText));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        /// <returns></returns>
        public string GetInformSystemHTML()
        {
            StringBuilder sb = new StringBuilder();
            //获取可以看到的且是书籍信息
            List<Inform> InfoList = InformDAL.m_InformDal.GetList(string.Format(" IsVisible=1 and InfoType=1 "));
            foreach (var Info in InfoList)
            {
                string sInformText = Info.InformText;
                sb.Append(string.Format("<p><a>{0}</a></p>", sInformText));
            }

            return sb.ToString();
        }
    }
}
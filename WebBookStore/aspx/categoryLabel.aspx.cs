using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class categoryLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(HttpContext.Current.Session["firstToSee"] == null)
            {
                Response.Redirect("/aspx/index.aspx");
            }
        }

        private string _askCategory;
        /// <summary>
        /// 显示类别
        /// </summary>
        public string AskCategory
        {
            get
            {
                try
                {
                    _askCategory = Request.QueryString["cate"] == null ? "" : Request.QueryString["cate"].ToString();
                }
                catch
                {
                    _askCategory = "";
                }
                return string.Format("{0}",_askCategory);
            }
            set { _askCategory = value; }
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
            BookDal.strWhere = string.Format("BookClassfication={0}", AskCategory);
            return BookDal.GetBooksHtml(page);
        }

        //获得分页控件
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
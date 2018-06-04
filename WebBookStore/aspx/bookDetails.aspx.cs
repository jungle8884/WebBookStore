using com.Utility;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class bookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["firstToSee"] == null)
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

        /// <summary>
        /// 返回图书详情页面
        /// </summary>
        /// <returns></returns>
        public string GetBooksDetailHtml()
        {
            return BookDetailsDAL.GetBookDetails(BookId);
        }

        /// <summary>
        /// 返回图书作者页面
        /// </summary>
        /// <returns></returns>
        public string GetBookAuthorIntroHtml()
        {
            return BookDetailsDAL.GetBookAuthor(BookId);
        }

        /// <summary>
        /// 返回图书评价页面
        /// </summary>
        /// <returns></returns>
        public string GetBookRemarksHtml()
        {
            return BookDetailsDAL.GetBookRemarks(BookId);
        }

        /// <summary>
        /// 获得图书出版社页面
        /// </summary>
        /// <returns></returns>
        public string GetBookPublisherHtml()
        {
            return BookDetailsDAL.GetBookPublisher(BookId);
        }

    }
}
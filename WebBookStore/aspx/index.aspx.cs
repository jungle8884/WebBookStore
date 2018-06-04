using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal;
using Model;
using System.Web.SessionState;

namespace WebBookStore
{
    public partial class index : System.Web.UI.Page, IRequiresSessionState
    {
        private static int bookSize = 64;//书籍总数

        protected void Page_Load(object sender, EventArgs e)
        {
            //保证必须先打开首页，不然会跳转至首页
            HttpContext.Current.Session["firstToSee"] = "Index.aspx";
        }

        /// <summary>
        /// 获得指定数量的书籍，见 bookSize
        /// </summary>
        /// <returns></returns>
        public string GetBooksHtml()
        {
            StringBuilder sb = new StringBuilder();
            List<Book> bookList = BookDal.m_BookDal.GetList("1=1", null, "BookId,BookTitle,BookImage,BookAuthor,BookTranslator,BookIntroduce", bookSize);
            foreach (var book in bookList)
            {
               sb.Append(BookDal.GetOneBookHtml(book));
            }
            return sb.ToString();
        }
    }
}
using Model;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebBookStore.ajax
{
    /// <summary>
    /// Searcher 的摘要说明
    /// </summary>
    public class Searcher : IHttpHandler
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
                case "searchBooks":
                    json = SearchBooks();
                    break;
            }
            context.Response.Write(json);
        }

        /// <summary>
        /// 从数据库中搜索数据并加载到搜素框中
        /// 类似于百度搜索框的效果
        /// </summary>
        /// <returns></returns>
        public string SearchBooks()
        {
            try
            {
                string searchWhat = context.Request.Form["searchKeywords"];
                StringBuilder sb = new StringBuilder();
                sb.Append(@"<ul>");
                string sql = string.Format("BookName like '%{0}%' or BookTitle like '%{0}%' or BookClassfication like '%{0}%'", searchWhat);
                List<Book> bookList = BookDal.m_BookDal.GetList(sql,null,"BookName");
                if (bookList.Count > 0)
                {
                    int count = 1;//计数器---可以保证最多加载几条数据
                    foreach (var book in bookList)
                    {
                        //最多显示几条数据
                        if (count == 6)
                            break;
                        sb.Append(string.Format(@"<li class=""search_content_li"" onclick=""SearchedContent(this)"">{0}</li>", book.BookName));
                        count++;
                    }
                }
                else
                {
                    sb.Append(@"<li class=""search_content_li"" onclick=""SearchedContent(this)"">没有相关书籍</li>");
                }
                sb.Append("</ul>");
                rm.Info = sb.ToString();
                return jss.Serialize(rm);
            }
            catch (Exception)
            {
                rm.Info = "未知错误";
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
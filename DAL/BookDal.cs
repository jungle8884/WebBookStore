using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using com.Utility;

namespace Dal
{
    /// <summary>
    /// 对书籍的数据访问
    /// </summary>
    public class BookDal : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<Book> m_BookDal = new BaseDAL<Book>();

        /// <summary>
        /// 书籍总数
        /// </summary>
        public static int bookSize()
        {
            return BookDal.m_BookDal.GetCount(strWhere);
        } 

        /// <summary>
        /// 每页显示多少
        /// </summary>
        public static int pageShowSize = 8;

        /// <summary>
        /// 查询条件
        /// 可以根据实际情况修改
        /// </summary>
        public static string strWhere = string.Format("1=1");

        /// <summary>
        /// 获得一本书籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static string GetOneBookHtml(Book book)
        {
            StringBuilder sb = new StringBuilder();
            if (book is null)
            {
                return "";
            }
            int bookId = book.BookId;
            //string bookTitle = book.BookTitle;
            //---------图书列表---介绍中最多显示的字符数---Start
            string bookTitle = book.BookTitle.Trim();
            int maxChars_bookTitle = 13;//最多显示字符数
            if (bookTitle.Length > maxChars_bookTitle)
            {
                bookTitle = bookTitle.Substring(0, maxChars_bookTitle) + "...";//最多显示maxChars个字符
            }
            //---------图书列表---介绍中最多显示的字符数---END
            string bookImgAddress = book.BookImage;
            string bookAuthor = book.BookAuthor;
            string bookTranslator = book.BookTranslator;
            //---------图书列表---介绍中最多显示的字符数---Start
            string bookbookIntro = book.BookIntroduce.Trim();
            int maxChars = 60;//最多显示字符数
            if (bookbookIntro.Length > maxChars)
            {
                bookbookIntro = bookbookIntro.Substring(0, maxChars) + "...";//最多显示maxChars个字符
            }
            //---------图书列表---介绍中最多显示的字符数---END

            #region 网页拼凑--书籍列表
            sb.Append(string.Format(@"<li class=""block-item"">
                                        <div class=""book-img"">
                                            <a href=""/aspx/bookDetails.aspx?bookId={7}"" title=""{0}"">
                                                <img src=""../imgs/{1}"" alt=""{2}""/>
                                            </a>
                                        </div>
                                        <div class=""book-info"">
                                            <h4 class=""name"">
                                                <a href=""bookDetails.aspx"" title=""{3}"">{3}</a>
                                            </h4>
                                            <div class=""author"">
                                                <span>
                                                    {4}                    &nbsp;
                                                </span>
                                                <span>
                                                    {5}  &nbsp;
                                                    译
                                                </span>
                                                <p class=""intro"">{6}</p>
                                            </div>
                                        </div>
                                    </li>", bookTitle, bookImgAddress, bookTitle, bookTitle, bookAuthor, bookTranslator, bookbookIntro, bookId));
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 获得指定页码的书籍
        /// </summary>
        /// <param name="page">指定页码，默认为 1 </param>
        /// <returns></returns>
        public static string GetBooksHtml(int page = 1)
        {
            StringBuilder sb = new StringBuilder();
            List<Book> bookList = BookDal.m_BookDal.GetList(strWhere, pageShowSize, page, "BookId,BookTitle,BookImage,BookAuthor,BookTranslator,BookIntroduce,BookClassfication");
            foreach (var book in bookList)
            {
                sb.Append(GetOneBookHtml(book));
            }
            return sb.ToString();
        }
    }
}

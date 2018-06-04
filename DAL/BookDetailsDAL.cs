using Model;
using com.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 对图书详情的操作
    /// 包括：图书介绍，出版社，作者等
    /// </summary>
    public class BookDetailsDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问--表Book
        /// </summary>
        public static BaseDAL<Book> m_BookDal = new BaseDAL<Book>();

        /// <summary>
        /// 此对象可以对数据库访问--表BookAuthor
        /// </summary>
        public static BaseDAL<BookAuthor> m_BookAuthorDal = new BaseDAL<BookAuthor>();

        /// <summary>
        /// 此对象可以对数据库访问--表BookRemark
        /// </summary>
        public static BaseDAL<BookRemark> m_BookRemarkDal = new BaseDAL<BookRemark>();

        /// <summary>
        /// 此对象可以对数据库访问--表BookRemarkReply
        /// </summary>
        public static BaseDAL<BookRemarkReply> m_BookRemarkReplyDal = new BaseDAL<BookRemarkReply>();

        /// <summary>
        /// 返回图书介绍
        /// </summary>
        /// <param name="BookId">图书ID</param>
        /// <returns></returns>
        public static string GetBookDetails(int BookId)
        {
            StringBuilder sb = new StringBuilder();

            #region 生明Book对象并赋值
            Book book = BookDetailsDAL.m_BookDal.GetModel(BookId);
            string bookName = book.BookName;
            string bookPublication = book.BookPublication;
            DateTime bookPublicTime = book.BookPublicTime;
            double bookPrice = book.BookPrice;
            string bookISBN = book.BookISBN;
            string bookTitle = book.BookTitle;
            string bookImage = book.BookImage;
            string bookAuthor = book.BookAuthor;
            string bookTranslator = book.BookTranslator;
            //string bookIntroduce = book.BookIntroduce;
            //---------图书列表---介绍中最多显示的字符数---Start
            string bookIntroduce = book.BookIntroduce.Trim();
            int maxChars = 180;//最多显示字符数
            if (bookIntroduce.Length > maxChars)
            {
                bookIntroduce = bookIntroduce.Substring(0, maxChars) + "...";//最多显示maxChars个字符
            }
            //---------图书列表---介绍中最多显示的字符数---END
            string bookClassfication = book.BookClassfication;
            #endregion

            #region 网页拼凑
            sb.Append(string.Format(@"
                    <div class=""row"">
                        <div class=""col-md-9"">
                            <div class=""col-md-4"">
                                <div class=""book-img"">
                                    <img src = ""../imgs/{0}"" alt=""{1}"" />
                                </div>
                            </div>
                            <div class=""col-md-8"">
                                <div class=""book-info"">
                                    <div class=""book-title"">
                                        <h2>{1}</h2>
                                        <div></div>
                                        <div><a></a></div>
                                    </div>
                                    <div class=""book-author"">
                                        <span>
                                            {2}(作者)
                                        </span>
                                        <span>
                                            {3}(译者)
                                        </span>
                                    </div>
                                    <div class=""book-intro readmore"" style=""max-height:none;height:100px;"">
                                        {4}
                                    </div>
                                    <div class=""book-price"">
                                        <dl>
                                            <dt style=""margin:10px auto;color: #e52222;"">价格</dt>
                                            <dd>
                                                <span class=""price"">
                                                    ￥{5}
                                                </span>
                                                <div class=""dbtn dbnJoin"" id=""dbnJoin""><a href=""javascript:;"">加入购物车</a></div>
                                                <div class=""dbtn dbnToCheck""><a href=""/aspx/ShoppingCart.aspx?bookId={6}"">去购物车结算</a></div>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-3"">
                            <div id=""dcar"">
                                <div id=""dprocount"">0</div>
                            </div>
                        </div>
                    </div>", bookImage, bookTitle, bookAuthor, bookTranslator, bookIntroduce, bookPrice, BookId));
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 返回图书作者
        /// 由图书的ID来获得数据的。
        /// <param name="BookId">图书ID</param>
        /// </summary>
        /// <returns></returns>
        public static string GetBookAuthor(int BookId)
        {
            StringBuilder sb = new StringBuilder();

            #region 生成BookAuthor对象并赋值
            List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ ParamName="@BookId", ParamValue=BookId}
            };
            BookAuthor ba = m_BookAuthorDal.GetModel("BookId=@BookId", list);

            if (ba == null) return "";//数据库暂时没那么多数据，数据录入完毕后删除此条代码

            string bookAuthorname = ba.BookAuthorName;
            string bookAuthorIntroduction = ba.BookAuthorIntroduction;

            #endregion

            #region 网页拼凑
            sb.Append(string.Format(@"<div class=""row""> 
                        <div class=""col-md-9"">
                            <div class=""block"">
                                 <div class=""block-header""><h3>作者介绍</h3></div>
                                 <div class=""block-body"">
                                   <div>
                                       {0}<br/>
                                       {1}
                                   </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-3""></div>
                    </div>", bookAuthorname, bookAuthorIntroduction));
            #endregion

            return sb.ToString();
        }
    
        /// <summary>
        /// 获得图书评价
        /// </summary>
        /// <param name="BookId">图书ID</param>
        /// <returns></returns>
        public static string GetBookRemarks(int BookId)
        {
            StringBuilder sb = new StringBuilder();

            List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ ParamName="@BookId", ParamValue=BookId}
            };
            List<BookRemark> BRlist = m_BookRemarkDal.GetList("BookId=@BookId", list);

            if (BRlist == null) return "";//数据库暂时没那么多数据，数据录入完毕后删除此条代码

            #region 网页拼凑
            foreach (var br in BRlist)
            {
                int UserId = br.UserId;
                User u = UserDal.m_UserDal.GetModel(UserId);
                if(u == null)
                {
                    return "";
                }
                string HeadPic = u.HeadPic;
                string UserName = u.UserName;
                DateTime CreatedTime = br.CreatedTime;
                int BookRemarkId = br.BookRemarkId;
                string BookRemarks = br.BookRemarks;

                #region 网页拼凑
                sb.Append("<li>");
                #region
                //图书评论
                sb.AppendFormat(@"<div class=""avatar"">
                                      <a href=""javascript:;"">
                                          <img src=""../upfile/HeadPic/{0}"" class=""avatar-img"" id=""avatar-img-{1}""/>
                                      </a>
                                  </div>
                                  <div class=""comment-txt"" id=""comment-content-{1}"">
                                        {2}
                                  </div>
                                  <div class=""comment-info"" id=""comment-info-{1}"">
                                       <a href=""javascript:;""> {3} <a>
                                       发表于: {4}
                                  </div>
                                  <div class=""action-bar"" id=""action-bar-{1}"">
                                       <a style=""display:none"" title=""{1}"" class=""Get_BookremarkId""></a>
                                       <a id=""reply-reply-{1}"" href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentShow({1})"">回复</a>
                                  </div>
                                  <div class=""input-group comment-btn"" id=""comment-btn-{1}"">
                                       <textarea class=""form-control comment-txtarea"" id=""comment-txtarea-{1}"" placeholder=""回复内容"" name=""commentContent""></textarea>
                                       <button type = ""button"" class=""btn txtarea-btn-submit"" id=""btnsubmit-{1}"" data-toggle=""modal"" onclick=""PostCommentReply({1})"">提交</button>
                                       <button type = ""button"" class=""btn txtarea-btn-cancel"" id=""btncancel-{1}"" data-toggle=""modal"" onclick=""PostCancel({1})"" >取消</button>
                                  </div>", HeadPic, BookRemarkId, BookRemarks, UserName, CreatedTime);
                #endregion

                sb.Append(@"<ul class=""comment-reply-ul"">");
                #region 图书评价的回复
                //获得对应图书评价的回复
                List<BookRemarkReply> brrList = BookDetailsDAL.m_BookRemarkReplyDal.GetList(string.Format("BookRemarkId={0}", BookRemarkId));
                if (brrList.Count > 0)
                {
                    foreach (var brr in brrList)
                    {
                        #region
                        //图书评论回复
                        sb.AppendFormat(@"<li style=""margin-left:10%;width:90%"">
                                              <div id=""reply-item-{0}"">
                                                  <p class=""comment-reply-txt"" id=""comment-reply-txt-{0}"">{1}</p>
                                                  <div class=""comment-reply-info"" id=""comment-reply-info-{0}"">
                                                     <a href=""javascript:void(0)"" class=""username"">{2}</a>
                                                      &nbsp;&nbsp;回复于 {3} 
                                                  </div>
                                                  <div class=""action-bar-reply"" id=""action-bar-reply-{0}"">
                                                        <a id=""reply-reply-{0}"" href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentReplyShow({0})"">回复</a>
                                                  </div>
                                                  <div class=""input-group comment-reply-btn"" id=""comment-reply-btn-{0}"">
                                                        <textarea class=""form-control reply-txtarea"" placeholder=""回复内容"" id=""reply-txtarea-{0}"" name=""Content""></textarea>
                                                        <button type=""button"" class=""btn reply-txtarea-btn-submit"" id=""btnsubmit-{0}"" data-toggle=""modal"" onclick=""PostCommentReplyAgain({0})"">提交</button>
                                                        <button type=""button"" class=""btn reply-txtarea-btn-cancel"" id=""btncancel-{0}"" data-toggle=""modal"" onclick=""PostReplyCancel({0})"">取消</button>
                                                  </div>
                                               </div>
                                            </li>", brr.BookRemarkReplyId, brr.BookRemarksReply, brr.UserName, brr.CreatedTime);
                        #endregion
                    }
                }
                #endregion
                sb.Append("</ul>");

                sb.Append("</li>");
                #endregion
            }
            #endregion

            return sb.ToString();
        }

        /// <summary>
        /// 获得图书出版社信息
        /// </summary>
        /// <param name="BookId">图书ID</param>
        /// <returns></returns>
        public static string GetBookPublisher(int BookId)
            {
                StringBuilder sb = new StringBuilder();

                Book book = BookDetailsDAL.m_BookDal.GetModel(BookId);
                string bookName = book.BookName;
                string bookPublication = book.BookPublication;
                DateTime bookPublicTime = book.BookPublicTime;
                double bookPrice = book.BookPrice;
                string bookISBN = book.BookISBN;
                string bookAuthor = book.BookAuthor;
                string bookTranslator = book.BookTranslator;

                sb.Append(string.Format(@"<div class=""block-body"">
                                        <ul class=""publish-info"">
                                            <li><strong>书 名</strong><span>{0}</span></li>
                                            <li><strong>出版日期</strong><span>{1}</span></li>
                                            <li><strong>书 号</strong><span>{2}</span></li>
                                            <li><strong>定 价</strong><span>{3}</span></li>
                                            <li><strong>出版社</strong><span>{4}</span></li>
                                            <li><strong>作 者</strong><span>{5}</span></li>
                                            <li><strong>译 者</strong><span>{6}</span></li>
                                      </ul>
                                    </div>", bookName, bookPublicTime.ToShortDateString(), bookISBN, bookPrice, bookPublication, book.BookAuthor, book.BookTranslator));
                return sb.ToString();
            }


    }

}
//<a id=""edit-reply-{1}""  href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentShow({1})"">编辑</a>
//<a id=""delete-reply-{1}""href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentShow({1})"">删除</a>
//<a id=""edit-reply-{0}"" href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentReplyShow({0})"">编辑</a>
//<a id=""delete-reply-{0}"" href=""javascript:void(0)"" class=""hover-show"" onclick=""PostCommentReplyShow({0})"">删除</a>
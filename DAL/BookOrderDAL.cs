using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class BookOrderDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<BookOrder> m_BookOrder = new BaseDAL<BookOrder>();

        /// <summary>
        /// 此对象可以对数据库中---Book访问
        /// </summary>
        private static BaseDAL<Book> m_BookDal = new BaseDAL<Book>();

        /// <summary>
        /// 查询条件
        /// 可以根据实际情况修改
        /// </summary>
        public static string strWhere = string.Format("1=1");

        /// <summary>
        /// 返回订单列表的网页内容
        /// </summary>
        /// <returns></returns>
        public static string getBookOrderListHTML()
        {
            StringBuilder sb = new StringBuilder();
            List<BookOrder> boList = m_BookOrder.GetList(strWhere);
            foreach (var bo in boList)
            {
                sb.Append(getOneBookOrderHTML(bo));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得一张订单的网页内容
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        private static string getOneBookOrderHTML(BookOrder bo)
        {
            StringBuilder sb = new StringBuilder();
            if (bo is null)
            {
                return "";
            }
            #region 数据准备
            int BookId = bo.BookId;
            int UserId = bo.UserId;
            int BookOrderId = bo.BookOrderId;
            int BookAmount = bo.BookAmount;
            double BookPrice = bo.BookPrice;
            string OrderNumber = bo.OrderNumber;
            //还需要的其他数据
            Book book = m_BookDal.GetModel(BookId);
            string bookImg = book.BookImage;
            string bookTitle = book.BookTitle;//bookTitle一般与alt一样
            //---------图书列表---介绍中最多显示的字符数---Start
            string bookIntro = book.BookIntroduce.Trim();
            int maxChars = 60;//最多显示字符数
            if (bookIntro.Length > maxChars)
            {
                bookIntro = bookIntro.Substring(0, maxChars) + "...";//最多显示maxChars个字符
            }
            //---------图书列表---介绍中最多显示的字符数---END
            string bookName = book.BookName;
            string bookISBN = book.BookISBN;
            string bookPublication = book.BookPublication;
            #endregion

            string[] sArray = OrderNumber.Split('-');
            string sYear = sArray[2];
            string sMonth = sArray[3];
            string sDay = sArray[4];
            if (DateTime.Today.Year == int.Parse(sYear))
            {
                if (DateTime.Today.Month == int.Parse(sMonth))
                {
                    if (DateTime.Today.Day == int.Parse(sDay))
                    {
                        #region 网页拼凑--订单列表
                        sb.Append(string.Format(@"<div class=""cartBox"">
                                        <div class=""order_content"">
                                            <ul class=""order_lists"">
                                                <li class=""list_con"">
                                                    <div class=""list_img"">
                                                        <a href=""javascript:;"">
                                                            <img src=""../imgs/{0}"" alt=""{1}"" />
                                                        </a>
                                                    </div>
                                                    <div class=""list_text"">
                                                        <a href=""javascript:;"">{2}</a>
                                                    </div>
                                                </li>
                                                <li class=""list_info"">
                                                    <p>书名：{3}</p>
                                                    <p>ISBN：{4}</p>
                                                    <p>出版社：{5}</p>
                                                    <p>订单号：{6}</p>
                                                </li>
                                                <li class=""list_price"">
                                                    <p class=""price"">￥{7}</p>
                                                </li>
                                                <li class=""list_amount"">
                                                    <div class=""amount_box"">
                                                        <input type=""text"" value=""{8}"" class=""sum"" />
                                                    </div>
                                                </li>
                                                <li class=""list_sum"">
                                                    <p class=""sum_price"" title=""{9}"">￥{9}</p>
                                                </li>
                                                <li class=""list_op"">
					                                <p class=""del""><a href=""javascript:;"" class=""delBtn"" title=""{10}"">取消订单</a></p>
				                                </li>
                                            </ul>
                                        </div>
                                    </div>", bookImg, bookTitle, bookIntro, bookName, bookISBN, bookPublication, OrderNumber, BookPrice, BookAmount, BookPrice * BookAmount, BookOrderId));
                        #endregion
                    }
                }
            }
            
            return sb.ToString();
        }
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 对购物车进行的操作
    /// </summary>
    public class ShoppingCartDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问--表ShoppingCart，用于购物车
        /// </summary>
        public static BaseDAL<ShoppingCart> m_ShoppingCart = new BaseDAL<ShoppingCart>();

        /// <summary>
        /// 此对象可以对数据库中---Book访问
        /// </summary>
        private static BaseDAL<Book> m_BookDal = new BaseDAL<Book>();

        /// <summary>
        /// 每页显示多少
        /// </summary>
        public static int ShoppingCartShowSize = 3;

        /// <summary>
        /// 当前用户购物车总数
        /// 默认等于每页数目，即只显示一页
        /// </summary>
        public static int ShoppingCartSize = ShoppingCartShowSize;

        /// <summary>
        /// 返回购物车页面代码
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="PageIndex">分页显示，当前页码</param>
        /// <returns></returns>
        public static string GetShoppingCartHtmls(int UserId, int PageIndex)
        {
            StringBuilder sb = new StringBuilder();
            ShoppingCartSize = ShoppingCartDAL.m_ShoppingCart.GetCount(string.Format("UserId={0}", UserId));//当前用户购物车总数
            List <ShoppingCart> scList = m_ShoppingCart.GetList(string.Format("UserId={0}",UserId), ShoppingCartShowSize, PageIndex);
            int checkbox_id = 1;
            foreach (var sc in scList)
            {
                sb.Append(GetOneShoppingCartHtmls(sc, checkbox_id));
                checkbox_id++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得一本书的购物车页面代码
        /// </summary>
        /// <param name="sc">购物车对象</param>
        /// <returns></returns>
        public static string GetOneShoppingCartHtmls(ShoppingCart sc, int checkbox_id)
        {
            StringBuilder sb = new StringBuilder();
            if(sc is null)
            {
                return "";
            }
            //购物车数据
            int userId = sc.UserId;
            int bookId = sc.BookId;
            string bookName = sc.BookName;
            //---------购物车列表---介绍中最多显示的字符数---Start
            string bookIntroduce = sc.BookIntroduce.Trim();
            int maxChars = 100;//最多显示字符数
            if (bookIntroduce.Length > maxChars)
            {
                bookIntroduce = bookIntroduce.Substring(0, maxChars) + "...";//最多显示maxChars个字符
            }
            //---------购物车列表---介绍中最多显示的字符数---END
            string bookISBN = sc.BookISBN;
            double bookPrice = sc.BookPrice;
            int bookAmount = sc.BookAmount;
            double bookTotalPrice = sc.BookTotalPrice;
            //还需要的其他数据
            Book book = m_BookDal.GetModel(bookId);
            string bookImg = book.BookImage;
            string bookTitle = book.BookTitle;//bookTitle一般与alt一样
            string bookPublication = book.BookPublication;

            #region 网页拼凑
            sb.Append(string.Format(@"<div class=""cartBox""> 
                                        <div class=""order_content"">
			                                <ul class=""order_lists"">
				                                <li class=""list_chk"">
					                                <input type=""checkbox"" id=""checkbox_{9}"" class=""son_check"" title=""{10}""/>
					                                <label for=""checkbox_{9}""></label>
				                                </li>
				                                <li class=""list_con"">
					                                <div class=""list_img""><a href=""javascript:;""><img src=""../imgs/{0}"" alt=""{1}""/></a></div>
					                                <div class=""list_text""><a href=""javascript:;"">{2}</a></div>
				                                </li>
				                                <li class=""list_info"">
					                                <p>书名：{3}</p>
                                                    <p>ISBN：{4}</p>
                                                    <p>出版社：{5}</p>
				                                </li>
				                                <li class=""list_price"">
					                                <p class=""price"" title=""{6}"">￥{6}</p>
				                                </li>
				                                <li class=""list_amount"">
					                                <div class=""amount_box"">
						                                <a href=""javascript:;"" class=""reduce reSty"">-</a>
						                                <input type=""text"" value=""{7}"" class=""sum""/>
						                                <a href=""javascript:;"" class=""plus"">+</a>
					                                </div>
				                                </li>
				                                <li class=""list_sum"">
					                                <p class=""sum_price"" title=""{8}"">￥{8}</p>
				                                </li>
				                                <li class=""list_op"">
					                                <p class=""del""><a href=""javascript:;"" class=""delBtn"">移除商品</a></p>
				                                </li>
			                                </ul>
		                                </div>
                                    </div>",
                                        bookImg, bookTitle, bookIntroduce, bookName, bookISBN, bookPublication, bookPrice, bookAmount, bookTotalPrice, checkbox_id, bookId));
            #endregion

            return sb.ToString();
        }
    }
}

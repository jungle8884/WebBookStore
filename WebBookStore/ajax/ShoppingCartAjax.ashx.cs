using Model;
using com.Utility;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace WebBookStore.ajax
{
    /// <summary>
    /// ShoppingCartAjax 的摘要说明
    /// </summary>
    public class ShoppingCartAjax : IHttpHandler, IRequiresSessionState
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
                case "AddInCart":
                    json = AddInCart();
                    break;
                case "CreateOrder":
                    json = CreateOrder();
                    break;
                case "DeleteOrder":
                    json = DeleteOrder();
                    break;
            }
            context.Response.Write(json);
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <returns></returns>
        public string AddInCart()
        {
            try
            {
                //检测是否登录
                if (UserDal.CurrentUser() != null)
                {
                    //声明对象并赋值
                    ShoppingCart sc = new ShoppingCart()
                    {
                        //在HeaderAjax.ashx页面登录时保存context.Session["UserId"]
                        //UserId = int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                        UserId = int.Parse(cookieHelper.GetCookie("UserId")),
                        //post传参
                        BookId = int.Parse(context.Request.Form["bookId"].ToString()),
                        BookName = context.Request.Form["bookName"].ToString(),
                        BookIntroduce = context.Request.Form["bookIntroduce"].ToString(),
                        BookISBN = context.Request.Form["bookISBN"].ToString(),
                        BookPrice = double.Parse(context.Request.Form["bookPrice"].ToString()),
                        BookAmount = int.Parse(context.Request.Form["bookAmount"].ToString()),
                        BookTotalPrice = double.Parse(context.Request.Form["bookTotalPrice"].ToString())
                    };
                    //将当前对象插入到数据库中，一个对象对应一条记录。(若先前有数据，则不插入)
                    if (ShoppingCartDAL.m_ShoppingCart.GetCount(string.Format("BookId={0} and UserId={1}", sc.BookId, sc.UserId)) == 0)
                        ShoppingCartDAL.m_ShoppingCart.Add(sc);
                    //状态信息
                    rm.Success = true;
                }
                else
                {
                    rm.Info = "加入购物车需要先登录!";
                }
            }
            catch (Exception)
            {
                rm.Info = "未知错误";
            }

            return jss.Serialize(rm);
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <returns></returns>
        public string CreateOrder() {
            try
            {
                //检测是否登录
                if (UserDal.CurrentUser() != null)
                {
                    int iBookId = int.Parse(context.Request.Form["bookId"].ToString());
                    Book book = BookDal.m_BookDal.GetModel(iBookId);
                    string sBookName = book.BookName;//图书名
                    int iUserId = int.Parse(cookieHelper.GetCookie("UserId"));
                    User user = UserDal.m_UserDal.GetModel(iUserId);
                    string sUserName = user.UserName;//用户名
                    int iBookAmount = int.Parse(context.Request.Form["bookAmount"].ToString());
                    double iBookPrice = double.Parse(context.Request.Form["bookPrice"].ToString());
                    //声明对象并赋值
                    BookOrder bookOrder = new BookOrder()
                    {
                        BookId = iBookId,
                        BookName = sBookName,
                        UserId = iUserId,
                        UserName = sUserName,
                        BookAmount = iBookAmount,
                        BookPrice = iBookPrice,
                        BookTotalPrice = iBookAmount * iBookPrice,
                        OrderNumber = iUserId.ToString() + "-" + iBookId.ToString() + "-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Millisecond
                    };
                    string strWhere = string.Format("BookId={0} and UserId={1}", bookOrder.BookId, bookOrder.UserId);
                    //将当前对象插入到数据库中，一个对象对应一条记录。
                    if (BookOrderDAL.m_BookOrder.GetCount(strWhere) > 0)
                    {
                        //若先前有数据则删除
                        BookOrderDAL.m_BookOrder.Delete(strWhere);
                        //然后再重新下单
                        BookOrderDAL.m_BookOrder.Add(bookOrder);
                    }
                    else
                    {
                        //否则直接下单
                        BookOrderDAL.m_BookOrder.Add(bookOrder);
                    }
                    //状态信息
                    rm.Success = true;
                }
                else
                {
                    rm.Info = "生成订单先需要先登录!";
                }
            }
            catch (Exception)
            {
                rm.Info = "未知错误";
            }

            return jss.Serialize(rm);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        public string DeleteOrder()
        {
            try
            {
                //检测是否登录
                if (UserDal.CurrentUser() != null)
                {
                    int iBookOrderId = int.Parse(context.Request.Form["iBookOrderId"].ToString());
                    string strWhere = string.Format(" BookOrderId={0} ", iBookOrderId);
                    //将当前对象插入到数据库中，一个对象对应一条记录。
                    if (BookOrderDAL.m_BookOrder.GetCount(strWhere) > 0)
                    {
                        //若先前有数据则删除
                        BookOrderDAL.m_BookOrder.Delete(strWhere);
                    }
                    //状态信息
                    rm.Success = true;
                }
                else
                {
                    rm.Info = "取消订单，先需要先登录!";
                }
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
using com.Utility;
using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WebBookStore.ajax
{
    /// <summary>
    /// BookDetailsAjax 的摘要说明
    /// </summary>
    public class BookDetailsAjax : IHttpHandler
    {
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
                case "addBookComment":
                    json = AddBookComment();
                    break;
                case "addBookCommentReply":
                    json = AddBookCommentReply();
                    break;
                case "addBookCommentReplyAgain":
                    json = AddBookCommentReplyAgain();
                    break;
            }
            context.Response.Write(json);
        }

        /// <summary>
        /// 添加图书评论的回复1
        /// </summary>
        /// <returns></returns>
        public string AddBookCommentReply()
        {
            int BookRemarkId = int.Parse(context.Request.Form["iBookRemarkId"].ToString());
            int BookId = int.Parse(context.Request.Form["iBookId"].ToString());
            User u = UserDal.CurrentUser();
            int UserId = u.UserId;
            string UserName = u.UserName;
            string BookRemarksReply = context.Request.Form["sBookRemarksReply"].ToString();

            rm.Success = true;
            try
            {
                BookRemarkReply bookRemarkReply = new BookRemarkReply();
                bookRemarkReply.BookRemarkId = BookRemarkId;
                bookRemarkReply.BookId = BookId;
                bookRemarkReply.UserId = UserId;
                bookRemarkReply.BookRemarksReply = BookRemarksReply;
                bookRemarkReply.UserName = UserName;
                bookRemarkReply.ClientIP = WebHelp.GetIP();
                bookRemarkReply.CreatedTime = DateTime.Now;
                if (CRegex.FilterHTML(bookRemarkReply.BookRemarksReply) == "")
                {
                    rm.Info = "内容不能为空";
                    return jss.Serialize(rm);
                }
                int iBookRemarkReplyId = BookDetailsDAL.m_BookRemarkReplyDal.Add(bookRemarkReply);
            }
            catch (Exception)
            {
                rm.Success = false;
                rm.Info = "未知错误";
            }
            return jss.Serialize(rm);
        }

        /// <summary>
        /// 添加图书评论的回复2
        /// </summary>
        /// <returns></returns>
        public string AddBookCommentReplyAgain()
        {
            int BookRemarkReplyId = int.Parse(context.Request.Form["iBookRemarkReplyId"].ToString());
            //对哪条回复的回复
            BookRemarkReply ReplyTo = BookDetailsDAL.m_BookRemarkReplyDal.GetModel(BookRemarkReplyId);
            int UserId = ReplyTo.UserId;
            string UserName = "@"+ ReplyTo.UserName;
            string sBookRemarksReplyAgain = context.Request.Form["sBookRemarksReplyAgain"].ToString();

            rm.Success = true;
            try
            {
                BookRemarkReply bookRemarkReply = new BookRemarkReply();
                bookRemarkReply.BookRemarkId = ReplyTo.BookRemarkId;
                bookRemarkReply.BookId = ReplyTo.BookId;
                bookRemarkReply.UserId = UserId;
                bookRemarkReply.BookRemarksReply = sBookRemarksReplyAgain;
                bookRemarkReply.UserName = UserName;
                bookRemarkReply.ClientIP = WebHelp.GetIP();
                bookRemarkReply.CreatedTime = DateTime.Now;
                if (CRegex.FilterHTML(bookRemarkReply.BookRemarksReply) == "")
                {
                    rm.Info = "内容不能为空";
                    return jss.Serialize(rm);
                }
                int iBookRemarkReplyId = BookDetailsDAL.m_BookRemarkReplyDal.Add(bookRemarkReply);
            }
            catch (Exception)
            {
                rm.Success = false;
                rm.Info = "未知错误";
            }
            return jss.Serialize(rm);
        }

        /// <summary>
        /// 添加图书评论
        /// </summary>
        /// <returns></returns>
        public string AddBookComment()
        {
            int BookId = int.Parse(context.Request.Form["iBookId"].ToString());
            User u = UserDal.CurrentUser();
            int UserId = u.UserId;
            string UserName = u.UserName;
            string BookRemarks = context.Request.Form["sBookRemarks"].ToString();

            rm.Success = true;
            try
            {
                BookRemark bookRemark = new BookRemark();
                bookRemark.BookId = BookId;
                bookRemark.UserId = UserId;
                bookRemark.BookRemarks = BookRemarks;
                bookRemark.UserName = UserName;
                bookRemark.ClientIP = WebHelp.GetIP();
                bookRemark.CreatedTime = DateTime.Now;
                if (CRegex.FilterHTML(bookRemark.BookRemarks) == "")
                {
                    rm.Info = "内容不能为空";
                    return jss.Serialize(rm);
                }
                int iBookRemarkId = BookDetailsDAL.m_BookRemarkDal.Add(bookRemark);
            }
            catch (Exception)
            {
                rm.Success = false;
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
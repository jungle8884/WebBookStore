using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 留言板
    /// </summary>
    public class WebCommentDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<WebComment> m_WebCommentDal = new BaseDAL<WebComment>();

        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        private static BaseDAL<User> m_UserDal = new BaseDAL<User>();

        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        private static BaseDAL<WebCommentReply> m_WebCommentReplyDAL = new BaseDAL<WebCommentReply>();

        /// <summary>
        /// 查询条件
        /// 可以根据实际情况修改
        /// </summary>
        public static string strWhere = string.Format("1=1");

        /// <summary>
        /// 评论总数
        /// </summary>
        /// <returns></returns>
        public static int CommentCount()
        {
            return m_WebCommentDal.GetCount(strWhere);
        }

        /// <summary>
        /// 每页显示多少
        /// </summary>
        public static int pageShowSize = 6;

        /// <summary>
        /// 返回所有的评论
        /// </summary>
        /// /// <param name="page">指定页码，默认为 1 </param>
        /// <returns></returns>
        public static string GetWebCommentHtmls(int page = 1)
        {
            StringBuilder sb = new StringBuilder();

            //List<WebComment> cList = m_WebCommentDal.GetList();
            List<WebComment> cList = m_WebCommentDal.GetList(strWhere, pageShowSize, page);

            int commentId = 0;
            foreach (var c in cList)
            {
                sb.Append(GetOneWebCommentHtml(c, commentId));
                commentId++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得一条评论
        /// </summary>
        /// <param name="webComment">当前评论对象</param>
        /// <param name="commentId">设置当前评论列表---div里的id，使得每个div都不重复</param>
        /// <returns></returns>
        private static string GetOneWebCommentHtml(WebComment webComment, int commentId)
        {
            StringBuilder sb = new StringBuilder();
            if (webComment is null)
            {
                return "";
            }

            int UserId = webComment.UserId;
            User u = m_UserDal.GetModel(webComment.UserId);
            string HeadPic = u.HeadPic;
            string UserName = u.UserName;

            string CommentTitle = webComment.CommentTitle;
            string CommentText = webComment.CommentText;
            DateTime CreatedTime = webComment.CreatedTime;

            #region 网页拼凑
            //当前帖子讨论数
            int count = m_WebCommentReplyDAL.GetCount(string.Format("WebCommentId={0}", webComment.WebCommentId));
            sb.Append(string.Format(@"<div class=""commentItem"" id=""commentId_{5}"">
                                        <div class=""itemLeft"">
                                            <img src = ""../upfile/HeadPic/{0}"" />
                                        </div>
                                        <div class=""itemCenter"">
                                            <div class=""itemCenter_1"">
                                                  <a href=""CommentReply.aspx?WebCommentId={6}"" target=""_blank"">{1}</a>
                                            </div>
                                            <div class=""itemCenter_2"">
                                                 <div class=""commentUser"">{2}</div>
                                                 <div class=""publishTime"">
                                                       发表于: {3}
                                                 </div>
                                            </div>
                                            <div class=""itemCenter_3"" id=""commentImgId_{5}"">
                                                {4}
                                            </div>
                                        </div>
                                        <div class=""itemRight"">
                                            <span class=""blue-color"">{7}</span>
                                            <p>讨论中(回帖数)</p>
                                        </div>
                                    </div>", HeadPic, CommentTitle, UserName, CreatedTime, CommentText, commentId, webComment.WebCommentId, count));

            #endregion

            return sb.ToString();
        }

    }
}

using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class CommentReply : System.Web.UI.Page
    {
        
        private int _WebCommentId;
        /// <summary>
        /// 当前帖子的ID
        /// </summary>
        public int WebCommentId
        {
            get
            {
                try
                {
                    _WebCommentId = Request.QueryString["WebCommentId"] == null ? 0 : Convert.ToInt32(Request.QueryString["WebCommentId"].ToString());
                }
                catch
                {
                    _WebCommentId = 0;
                }
                return _WebCommentId;
            }
            set { _WebCommentId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //设置 <input type="hidden" value="0" id="hiddenWebCommentId"  runat="server"/> 的value值
            hiddenWebCommentId.Value = WebCommentId.ToString();
            //防止直接打开留言回复网页
            if(WebCommentId == 0)
            {
                Response.Redirect("/aspx/Board.aspx");
            }
        }

        /// <summary>
        /// 返回当前帖子
        /// </summary>
        /// <param name="webCommentId"></param>
        /// <returns></returns>
        public string GetCurrentCommentbyCommentId()
        {
            return CommentReplyDAL.GetCurrentCommentbyCommentId(WebCommentId);
        }

        /// <summary>
        /// 返回该帖子的评论列表
        /// </summary>
        /// <returns></returns>
        public string GetCurrentCommentReplybyCommentId()
        {
            return CommentReplyDAL.GetCurrentCommentReplybyCommentId(WebCommentId);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WebCommentReply : BaseModel
    {
        public WebCommentReply()
        {
            PrimaryKey = "WebCommentReplyId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        /// <summary>
        /// 对于当前留言的回复
        /// </summary>
        public int WebCommentReplyId { get; set; }

        /// <summary>
        /// 当前留言
        /// </summary>
        public int WebCommentId { get; set; }
        
        /// <summary>
        /// 对当前帖子回复的用户Id
        /// </summary>
        public int UserId { get; set; }

        public string CommentReplyText { get; set; }

        public DateTime CreatedTime { get; set; }

        public string ClientIP { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WebComment : BaseModel
    {
        public WebComment()
        {
            PrimaryKey = "WebCommentId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int WebCommentId { get; set; }

        /// <summary>
        /// 发帖子的用户
        /// </summary>
        public int UserId { get; set; }

        public string CommentTitle { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedTime { get; set; }

        public string ClientIP { get; set; }

    }
}

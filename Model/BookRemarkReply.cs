using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 图书评论的回复类
    /// </summary>
    public class BookRemarkReply : BaseModel
    {
        public BookRemarkReply()
        {
            PrimaryKey = "BookRemarkReplyId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int BookRemarkReplyId { get; set; }

        //确定对哪条评论回复
        public int BookRemarkId { get; set; }

        //确定回复的哪本书
        public int BookId { get; set; }

        //谁回复的这本书
        public int UserId { get; set; }

        //图书评价的回复
        public string BookRemarksReply { get; set; }

        //谁回复的这本书
        public string UserName { get; set; }

        public DateTime CreatedTime { get; set; }

        public string ClientIP { get; set; }

    }
}

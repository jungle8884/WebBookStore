using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookRemark : BaseModel
    {
        public BookRemark()
        {
            PrimaryKey = "BookRemarkId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int BookRemarkId { get; set; }
        
        public int BookId { get; set; }

        public int UserId { get; set; }

        //图书评价
        public string BookRemarks { get; set; }

        //谁评价的这本书
        public string UserName { get; set; }

        public DateTime CreatedTime { get; set; }

        public string ClientIP { get; set; }
    }
}

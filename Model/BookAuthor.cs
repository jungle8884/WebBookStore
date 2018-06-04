using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookAuthor : BaseModel
    {
        public BookAuthor()
        {
            PrimaryKey = "BookAuthorId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }
        
        public int BookAuthorId { get; set; }

        public int BookId { get; set; }

        public string BookAuthorName { get; set; }

        public string BookName { get; set; }

        public string BookAuthorIntroduction { get; set; }
    }
}

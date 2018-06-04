using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book : BaseModel
    {
        public Book()
        {
            PrimaryKey = "bookId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string BookPublication { get; set; }

        public DateTime BookPublicTime { get; set; }

        public double BookPrice { get; set; }

        public string BookISBN { get; set; }

        public string BookTitle { get; set; }

        public string BookImage { get; set; }

        public string BookAuthor { get; set; }

        public string BookTranslator { get; set; }

        public string BookIntroduce { get; set; }

        public string BookClassfication { get; set; }
    }
}

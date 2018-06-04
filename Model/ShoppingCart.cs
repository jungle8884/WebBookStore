using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 购物车类
    /// </summary>
    public class ShoppingCart : BaseModel
    {
        public ShoppingCart()
        {
            PrimaryKey = "ShoppingCartId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int ShoppingCartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string BookIntroduce { get; set; }

        public string BookISBN { get; set; }

        public double BookPrice { get; set; }

        public int BookAmount { get; set; }

        public double BookTotalPrice { get; set; }

    }
}
//注意 BookPrice 与数据库对应的类型（数据库为float而Model里为double）
//SQL Server没有double类型，若是你需要用双精度数据，如果不固定小数位，用float就可以了;
//若是固定小数位，可以用numric;如果整数和小数都出现，可以用real

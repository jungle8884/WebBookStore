using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 一本书生成一个订单
    /// </summary>
    public class BookOrder : BaseModel
    {
        public BookOrder()
        {
            PrimaryKey = "BookOrderId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int BookOrderId { get; set; }

        public int BookId { get; set; }
        /// <summary>
        /// 图书名
        /// </summary>
        public string BookName { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// 当前订单的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 图书数量
        /// </summary>
        public int BookAmount { get; set; }

        /// <summary>
        /// 图书价格
        /// </summary>
        public double BookPrice { get; set; }

        /// <summary>
        /// 图书总价格
        /// </summary>
        public double BookTotalPrice { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

    }
}

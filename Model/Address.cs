using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Address : BaseModel
    {
        public Address()
        {
            PrimaryKey = "AddressId";
            DataBaseName = DataBaseEnum.WebBookStore;
        }

        public int AddressId { get; set; }

        public int UserId { get; set; }

        public string Recipient { get; set; }

        public string DetailedAddress { get; set; }

        /// <summary>
        /// 是否为默认地址
        /// 0 代表不是
        /// 1 代表是
        /// </summary>
        public int IsDefaultOrNot { get; set; }

        public string Tel { get; set; }

    }
}

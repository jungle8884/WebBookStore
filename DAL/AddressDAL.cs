using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 对地址的数据访问
    /// </summary>
    public class AddressDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<Address> m_AddressDal = new BaseDAL<Address>();
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// 公告栏
    /// </summary>
    public class InformDAL : System.Web.UI.Page
    {
        /// <summary>
        /// 此对象可以对数据库访问
        /// </summary>
        public static BaseDAL<Inform> m_InformDal = new BaseDAL<Inform>();
    }
}

using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.my
{
    public partial class ManageAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserDal.CurrentUser() == null)
            {
                Response.Redirect("/aspx/index.aspx");
            }
        }

        /// <summary>
        /// 获得选择地址框
        /// </summary>
        /// <returns></returns>
        public string getSelectionHTML()
        {
            StringBuilder sb = new StringBuilder();
            List<Model.Address> addrList = AddressDAL.m_AddressDal.GetList(string.Format(" UserId={0} ", UserDal.CurrentUser().UserId));

            foreach (var addr in addrList)
            {
                sb.Append(string.Format(@"<option class=""txt sm w-sm"" value=""{0}"">{1}</option>", addr.AddressId, addr.DetailedAddress));
            }

            return sb.ToString();
        }

    }
}
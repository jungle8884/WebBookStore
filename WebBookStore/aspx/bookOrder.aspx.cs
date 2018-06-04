using com.Utility;
using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBookStore.aspx
{
    public partial class bookOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserDal.CurrentUser() == null)
            {
                Response.Redirect("/aspx/index.aspx");
            }
        }

        /// <summary>
        /// 返回订单列表的网页内容
        /// </summary>
        /// <returns></returns>
        public string GetOrderListHTML()
        {
            int UserId = int.Parse(cookieHelper.GetCookie("UserId"));//若有，一定大于0
            if (UserId > 0)
            {
                BookOrderDAL.strWhere = string.Format(" UserId={0} ", UserId);
                return BookOrderDAL.getBookOrderListHTML();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获得收货地址
        /// </summary>
        /// <returns></returns>
        public string GetAddressHTML()
        {
            StringBuilder sb = new StringBuilder();
            List<Address> addrList = AddressDAL.m_AddressDal.GetList(string.Format(" UserId={0}", UserDal.CurrentUser().UserId));
            foreach (var addr in addrList)
            {
                string[] sArray = addr.DetailedAddress.Split('-');
                string prov = sArray[0].ToString();
                string city = sArray[1].ToString();
                string dist = sArray[2].ToString();
                string town = sArray[3].ToString();
                string street = sArray[4].ToString();

                string phone = addr.Tel;
                string recipient = addr.Recipient;
                int IsDefaultOrNot = addr.IsDefaultOrNot;

                #region 网页拼凑
                sb.Append(string.Format(@"<div class=""addr"">
                                             <div class=""inner"">
                                                 <div class=""addr-hd"">
                                                     <span class=""prov"">{0}</span>
                                                     <span class=""city"">{1}</span>
                                                     <span>（</span>
                                                     <span>{2}</span>
                                                     <span>收）</span>
                                                 </div>
                                                 <div class=""addr-hd"">
                                                     <span class=""dist"">{3}</span>
                                                     <span class=""town"">{4}</span>
                                                     <span class=""street"">{5}</span>
                                                     <span class=""phone"">{6}</span>
                                                 </div>
                                                 <div class=""curMarker"" title=""{7}""></div>
                                                 <div class=""defaultTip"" title=""{8}"">设为默认</div>
                                             </div>
                                          </div>", prov, city, recipient, dist, town, street, phone, addr.AddressId, IsDefaultOrNot)); 
                #endregion
            }

            return sb.ToString();
        }
    }
}
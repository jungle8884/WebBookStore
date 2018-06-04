using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using Model;
using Dal;

namespace WebBookStore.ajax
{
    /// <summary>
    /// MyIndexAjax 的摘要说明
    /// </summary>
    public class MyIndexAjax : IHttpHandler
    {
        private JavaScriptSerializer m_JavaScriptSerializer = new JavaScriptSerializer();
        string json = "";
        HttpContext context;
        ReturnMessage rMessage = new ReturnMessage();

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Request.ContentEncoding = Encoding.GetEncoding("utf-8"); //必须加上，否则会产生乱码
            //接收浏览器 get/post 过来的参数cmd
            string cmd = context.Request["cmd"].ToString();

            switch (cmd)
            {
                case "showinfor":
                    json = ShowInfor();
                    break;
                case "updateinfor":
                    json = UpdateInfor();
                    break;
                case "addAddress":
                    json = AddAddress();
                    break;
                case "setDefaultAddress":
                    json = SetDefaultAddress();
                    break;
                case "manageAddress":
                    json = ManageAddress();
                    break;
                case "saveAddress":
                    json = SaveAddress();
                    break;
                case "deleteAddress":
                    json = DeleteAddress();
                    break;
            }
            context.Response.Write(json);
        }

        public string UpdateInfor()
        {
            string email = context.Request["email"].ToString();
            string phonenum = context.Request["phonenum"].ToString();
            string qq = context.Request["qq"].ToString();
            string gender = context.Request["gender"].ToString();
            User user = UserDal.CurrentUser();
            user.Email = email;
            user.Tel = phonenum;
            user.QQ = qq;
            user.Gender = gender;
            UserDal.m_UserDal.Update(user);
            rMessage.Info = "信息修改成功";
            return m_JavaScriptSerializer.Serialize(rMessage);
        }

        public string ShowInfor()
        {
            return m_JavaScriptSerializer.Serialize(UserDal.CurrentUser());
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <returns></returns>
        public string AddAddress()
        {
            string sRecipient = context.Request.Form["sRecipient"].ToString();
            string sAddress = context.Request.Form["sAddress"].ToString();
            string sTel = context.Request.Form["sTel"].ToString();
            if (sRecipient != "" || sAddress != "" || sTel != "")
            {
                Address addr = new Address();
                addr.Recipient = sRecipient;
                addr.DetailedAddress = sAddress;
                addr.IsDefaultOrNot = 0;//一般默认为 0 即不是默认收货地址
                addr.Tel = sTel;
                addr.UserId = UserDal.CurrentUser().UserId;

                if (AddressDAL.m_AddressDal.GetCount(string.Format(" UserId={0}", addr.UserId)) >= 4)
                {
                    rMessage.Success = false;
                    rMessage.Info = "收货地址最多只能添加四个";
                }
                else
                {
                    AddressDAL.m_AddressDal.Add(addr);
                    rMessage.Success = true;
                    rMessage.Info = "收货地址添加成功";
                }
            }
            else
            {
                rMessage.Success = false;
                rMessage.Info = "地址信息不能为空";
            }

            return m_JavaScriptSerializer.Serialize(rMessage);
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <returns></returns>
        public string SetDefaultAddress()
        {
            int iAddressId = int.Parse(context.Request.Form["iSelId"].ToString());
            Address setDefaultAddr = AddressDAL.m_AddressDal.GetModel(string.Format(" AddressId={0} ", iAddressId));
            setDefaultAddr.IsDefaultOrNot = 1;//设为默认地址
            if (AddressDAL.m_AddressDal.Update(setDefaultAddr))
            {
                rMessage.Success = true;
                rMessage.Info = "成功设为默认地址";
                List<Address> addrList = AddressDAL.m_AddressDal.GetList(string.Format(" UserId={0} ", setDefaultAddr.UserId));
                foreach (var addr in addrList)
                {
                    if (addr.AddressId != iAddressId)
                    {
                        //当前用户的其它地址设为--不是默认地址
                        addr.IsDefaultOrNot = 0;
                        AddressDAL.m_AddressDal.Update(addr);
                    }
                }
            }
            else
            {
                rMessage.Success = false;
                rMessage.Info = "设置失败";
            }

            return m_JavaScriptSerializer.Serialize(rMessage);
        }

        /// <summary>
        /// 管理收获地址
        /// </summary>
        /// <returns></returns>
        public string ManageAddress()
        {
            int iAddressId = int.Parse(context.Request.Form["iSelId"].ToString());
            Address manageAddr = AddressDAL.m_AddressDal.GetModel(string.Format(" AddressId={0} ", iAddressId));

            return m_JavaScriptSerializer.Serialize(manageAddr);
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <returns></returns>
        public string SaveAddress()
        {
            int iSelId = int.Parse(context.Request["iSelId"].ToString());
            if (iSelId > 0)
            {
                Address addr = AddressDAL.m_AddressDal.GetModel(iSelId); ;
                addr.Tel = context.Request["sManage_Tel"].ToString();
                addr.Recipient = context.Request["sManage_Recipient"].ToString();
                addr.DetailedAddress = context.Request["sManage_Address"].ToString();
                if (AddressDAL.m_AddressDal.Update(addr))
                {
                    rMessage.Success = true;
                    rMessage.Info = "地址信息修改成功";
                }
                else
                {
                    rMessage.Success = false;
                    rMessage.Info = "地址信息修改失败";
                }
            }
            else
            {
                rMessage.Success = false;
                rMessage.Info = "地址信息修改失败";
            }

            return m_JavaScriptSerializer.Serialize(rMessage);
        }

        /// <summary>
        /// 删除当前收货地址
        /// </summary>
        /// <returns></returns>
        public string DeleteAddress()
        {
            int iSelId = int.Parse(context.Request.Form["iSelId"].ToString());
            if (AddressDAL.m_AddressDal.Delete(iSelId) > 0)
            {
                rMessage.Success = true;
                rMessage.Info = "地址信息删除成功";
            }
            else
            {
                rMessage.Success = false;
                rMessage.Info = "地址信息删除失败";
            }

            return m_JavaScriptSerializer.Serialize(rMessage);
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}
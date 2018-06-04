using System;
using System.Collections.Generic;
using System.Text;

namespace com.Utility
{
    public class Md5Class
    {
        /// <summary>
        /// ��ȡMD5���ܺ������
        /// </summary>
        /// <param name="myString"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMD5(string myString, int code)
        {
            if (code == 16) //16λMD5���ܣ�ȡ32λ���ܵ�9~25�ַ��� 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(myString, "MD5").ToLower().Substring(8, 16);
            }
            else//32λ���� 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(myString, "MD5").ToLower();
            }
        }   
    }
}

using System;
using System.Net;
using System.Net.Mail;
namespace com.Utility
{
    /// <summary>
    /// CEmailSend 的摘要说明。
    /// </summary>
    public class CEmailSend
    {
        private string sfrom = "";//发件人邮箱地址
        private string sfromer = "";//发件人名称
        private string stoer = "";//收件人名称
        private string sSMTPHost = "";//邮件服务器地址如：smtp.sohu.com
        private string sSMTPuser = "";//发件人邮箱地址
        private string sSMTPpass = "";//独立密码
        /// <summary>
        /// C#发送邮件函数
        /// </summary>
        /// <param name="to">接受者邮箱</param>
        /// <param name="Subject">主题</param>
        /// <param name="Body">内容</param>
        /// <param name="file">附件</param>
        ///<param name="cc">抄送</param>
        /// <returns></returns>
        public bool SendEmail(string sto, string sSubject, string sBody, string sfile,string cc)
        {
            ////设置from和to地址
            MailAddress from = new MailAddress(sfrom, sfromer);
            MailAddress to = new MailAddress(sto, stoer);

            ////创建一个MailMessage对象
            MailMessage oMail = new MailMessage(from, to);

            //// 添加附件
            if (sfile != "")
            {
                oMail.Attachments.Add(new Attachment(sfile));
            }
            ////邮件标题
            oMail.Subject = sSubject;

            ////邮件内容
            oMail.Body = sBody;

            ////邮件格式
            oMail.IsBodyHtml = false;

            ////邮件采用的编码
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

            ////设置邮件的优先级为高
            oMail.Priority = MailPriority.High;

            if (cc != "")
            {
                if (cc.ToLower().IndexOf(';') > 0)
                {
                    cc = cc.Substring(0, cc.Length - 1);
                    string[] acc = cc.Split(';');
                    foreach (var c in acc)
                    {
                        oMail.CC.Add(c);
                    }
                }
                else
                {
                    oMail.CC.Add(cc);
                }
            }
            ////发送邮件
            SmtpClient client = new SmtpClient();
            ////client.UseDefaultCredentials = false; 
            client.Host = sSMTPHost;
            client.Credentials = new NetworkCredential(sSMTPuser, sSMTPpass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(oMail);
                return true;
            }
            catch (Exception err)
            {
                string s = err.Message;
                return false;
            }
            finally
            {
                ////释放资源
                oMail.Dispose();
            }
        }
    }
}

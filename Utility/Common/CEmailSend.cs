using System;
using System.Net;
using System.Net.Mail;
namespace com.Utility
{
    /// <summary>
    /// CEmailSend ��ժҪ˵����
    /// </summary>
    public class CEmailSend
    {
        private string sfrom = "";//�����������ַ
        private string sfromer = "";//����������
        private string stoer = "";//�ռ�������
        private string sSMTPHost = "";//�ʼ���������ַ�磺smtp.sohu.com
        private string sSMTPuser = "";//�����������ַ
        private string sSMTPpass = "";//��������
        /// <summary>
        /// C#�����ʼ�����
        /// </summary>
        /// <param name="to">����������</param>
        /// <param name="Subject">����</param>
        /// <param name="Body">����</param>
        /// <param name="file">����</param>
        ///<param name="cc">����</param>
        /// <returns></returns>
        public bool SendEmail(string sto, string sSubject, string sBody, string sfile,string cc)
        {
            ////����from��to��ַ
            MailAddress from = new MailAddress(sfrom, sfromer);
            MailAddress to = new MailAddress(sto, stoer);

            ////����һ��MailMessage����
            MailMessage oMail = new MailMessage(from, to);

            //// ��Ӹ���
            if (sfile != "")
            {
                oMail.Attachments.Add(new Attachment(sfile));
            }
            ////�ʼ�����
            oMail.Subject = sSubject;

            ////�ʼ�����
            oMail.Body = sBody;

            ////�ʼ���ʽ
            oMail.IsBodyHtml = false;

            ////�ʼ����õı���
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

            ////�����ʼ������ȼ�Ϊ��
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
            ////�����ʼ�
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
                ////�ͷ���Դ
                oMail.Dispose();
            }
        }
    }
}

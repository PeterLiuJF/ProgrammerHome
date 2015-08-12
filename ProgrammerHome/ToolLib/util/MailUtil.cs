using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Mail;
using System.Configuration;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-17 
 * Content: �ʼ���
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// �ʼ���
    /// </summary>
    public class MailUtil
    {
        #region ��/ͬ�������ʼ�
        /// <summary>
        /// ��/ͬ�������ʼ�
        /// ע�������ҳ������첽����������Ҫ����ҳ�� Page ������ Async="true"
        /// </summary>
        /// <param name="strMailFromAddress">�����������ַ</param>
        /// <param name="strMailDisplayName">��������ʾ����</param>
        /// <param name="strMailFromPwd">��������������</param>
        /// <param name="arrMailToAddress">�ռ��������ַ</param>
        /// <param name="arrMailCcAddress">�����������ַ(��Ϊ null)</param>
        /// <param name="strSmtpServer">Smtp��������ַ("mail.bhinfoLib.com")</param>
        /// <param name="strSubject">�ʼ�����</param>
        /// <param name="strBody">�ʼ�����(��Ϊ ��)</param>
        /// <param name="arrMailAttachment">����(��Ϊ null)</param>
        /// <param name="boolIsBodyHtml">�ʼ������Ƿ�HTML��ʽ(false|true)</param>
        /// <param name="mailPriority">�ʼ����ȼ�(MailPriority.Low|MailPriority.Normal|MailPriority.High)</param>
        /// <param name="mailEncoding">�ʼ���������ı���(��Ϊ null;Ϊ nullʱĬ��System.Text.Encoding.UTF8)</param>
        /// <param name="Async">�Ƿ����첽�����ʼ�(true|false)</param>
        public static void SendMail(string strMailFromAddress, string strMailDisplayName, string strMailFromPwd, string[] arrMailToAddress, string[] arrMailCcAddress, string strSmtpServer, string strSubject, string strBody, string[] arrMailAttachment, bool boolIsBodyHtml, MailPriority mailPriority, Encoding mailEncoding, bool Async)
        {
            #region ������֤

            //�����˵�ַ����Ϊ��
            if (string.IsNullOrEmpty(strMailFromAddress))
            {
                return;
            }

            //��������ʾ����Ϊ��ʱ��Ĭ����ʾ�����˵�ַ
            if (string.IsNullOrEmpty(strMailDisplayName))
            {
                strMailDisplayName = strMailFromAddress;
            }

            //���������벻��Ϊ��
            if (string.IsNullOrEmpty(strMailFromPwd))
            {
                return;
            }

            //�ռ��˵�ַ����Ϊ��
            if (arrMailToAddress == null)
            {
                return;
            }

            //Smtp��������ַ����Ϊ��
            if (string.IsNullOrEmpty(strSmtpServer))
            {
                return;
            }

            //�ʼ����ⲻ��Ϊ��
            if (string.IsNullOrEmpty(strSubject))
            {
                return;
            }

            //Ĭ���ʼ�����Ϊ UTF8
            if (mailEncoding == null)
            {
                mailEncoding = System.Text.Encoding.UTF8;
            }

            //Ĭ���ʼ����Ĳ���HTML��ʽ
            //if (boolIsBodyHtml == null)
            //{
            //    boolIsBodyHtml = false;
            //}           

            //Ĭ���ʼ����ȼ�Ϊ ����
            //if (mailPriority == null)
            //{
            //    mailPriority = MailPriority.Normal;
            //}

            //Ĭ�����첽��ʽ�����ʼ�
            //if (Async == null)
            //{
            //    Async = true;
            //}
            #endregion

            #region MailMessage

            MailMessage mailMsg = new MailMessage();

            //�����˵�ַ
            mailMsg.From = new MailAddress(strMailFromAddress, strMailDisplayName, mailEncoding);

            //�ռ��˵�ַ
            for (int i = 0; i < arrMailToAddress.Length; i++)
            {
                mailMsg.To.Add(arrMailToAddress[i]);
            }

            //�����˵�ַ
            if (arrMailCcAddress != null)
            {
                for (int i = 0; i < arrMailCcAddress.Length; i++)
                {
                    mailMsg.CC.Add(arrMailCcAddress[i]);
                }
            }

            //�ʼ�����
            mailMsg.Subject = strSubject;

            //�ʼ�������� 
            mailMsg.SubjectEncoding = mailEncoding;

            //�ʼ�����
            mailMsg.Body = strBody;

            //�ʼ����ı��� 
            mailMsg.BodyEncoding = mailEncoding;

            //���и���
            if (arrMailAttachment != null)
            {
                for (int i = 0; i < arrMailAttachment.Length; i++)
                {
                    mailMsg.Attachments.Add(new Attachment(arrMailAttachment[i]));
                }
            }

            //�ʼ����ȼ�
            mailMsg.Priority = mailPriority;

            //�ʼ������Ƿ���HTML��ʽ
            mailMsg.IsBodyHtml = boolIsBodyHtml;

            #endregion

            #region SmtpClient

            SmtpClient smtpClient = new SmtpClient();

            //��֤�������û���������
            smtpClient.Credentials = new System.Net.NetworkCredential(strMailFromAddress, strMailFromPwd);

            //Smtp��������ַ
            smtpClient.Host = strSmtpServer;

            object userState = mailMsg;

            try
            {
                if (Async)
                {
                    //�첽�����ʼ�
                    smtpClient.SendAsync(mailMsg, userState);
                }
                else
                {
                    //ͬ�������ʼ�
                    smtpClient.Send(mailMsg);
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //�����쳣
                throw ex;
            }

            #endregion

        }
        #endregion

        #region ��/ͬ�������ʼ�
        /// <summary>
        /// ��/ͬ�������ʼ�
        /// ע�������ҳ������첽����������Ҫ����ҳ�� Page ������ Async="true"
        /// </summary>
        /// <param name="strMailFromAddress">�����������ַ</param>
        /// <param name="strMailFromPwd">��������������</param>
        /// <param name="arrMailToAddress">�ռ��������ַ</param>
        /// <param name="strSmtpServer">Smtp��������ַ("mail.bhinfoLib.com")</param>
        /// <param name="strSubject">�ʼ�����</param>
        /// <param name="strBody">�ʼ�����(��Ϊ ��)</param>
        public static void SendMail(string strMailFromAddress, string strMailFromPwd, string[] arrMailToAddress, string strSmtpServer, string strSubject, string strBody)
        {
            SendMail(strMailFromAddress, null, strMailFromPwd, arrMailToAddress, null, strSmtpServer, strSubject, strBody, null, false, MailPriority.Normal, System.Text.Encoding.UTF8, true);
        }
        #endregion

        #region ��/ͬ�������ʼ�
        /// <summary>
        ///  ��/ͬ�������ʼ�
        ///  ע��1.�����ҳ������첽����������Ҫ����ҳ�� Page ������ Async="true"
        ///      2.�ʼ���Ϣ��Ҫ��Web.config�ļ�������
        ///      <add key="MailFromAddress" value="hehl@bhinfoLib.com"/>
        ///      <add key="MailDisplayName" value="����"/>
        ///      <add key="MailFromPwd" value="hehl"/>
        ///      <add key="MailToAddress" value="hehl@bhinfoLib.com;hehl_231@126.com"/>
        ///      <add key="MailCcAddress" value="hehl@bhinfoLib.com;hehl_231@126.com"/>
        ///      <add key="SmtpServer" value="mail.bhinfoLib.com"/>		
        /// </summary>
        /// <param name="strSubject">�ʼ�����</param>
        /// <param name="strBody">�ʼ�����(��Ϊ ��)</param>
        public static void SendMail(string strSubject, string strBody)
        {
            string strMailFromAddress = "";
            string strMailDisplayName = "";
            string strMailFromPwd = "";
            string[] arrMailToAddress = null;
            string[] arrMailCcAddress = null;
            string strSmtpServer = "";

            strMailFromAddress = ConfigurationManager.AppSettings["MailFromAddress"];
            strMailDisplayName = ConfigurationManager.AppSettings["MailDisplayName"];
            strMailFromPwd = ConfigurationManager.AppSettings["MailFromPwd"];

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MailToAddress"]))
            {
                arrMailToAddress = ConfigurationManager.AppSettings["MailToAddress"].Split(';');
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MailCcAddress"]))
            {
                arrMailCcAddress = ConfigurationManager.AppSettings["MailCcAddress"].Split(';');
            }
            strSmtpServer = ConfigurationManager.AppSettings["SmtpServer"];

            SendMail(strMailFromAddress, strMailDisplayName, strMailFromPwd, arrMailToAddress, null, strSmtpServer, strSubject, strBody, null, false, MailPriority.Normal, System.Text.Encoding.UTF8, true);
        }
        #endregion
    }
}

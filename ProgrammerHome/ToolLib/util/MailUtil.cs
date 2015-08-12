using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Mail;
using System.Configuration;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-17 
 * Content: 邮件类
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// 邮件类
    /// </summary>
    public class MailUtil
    {
        #region 异/同步发送邮件
        /// <summary>
        /// 异/同步发送邮件
        /// 注：如果是页面调用异步方法，则需要设置页面 Page 的属性 Async="true"
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailDisplayName">发件人显示名称</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="arrMailToAddress">收件人邮箱地址</param>
        /// <param name="arrMailCcAddress">抄送人邮箱地址(可为 null)</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.bhinfoLib.com")</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        /// <param name="arrMailAttachment">附件(可为 null)</param>
        /// <param name="boolIsBodyHtml">邮件正文是否HTML格式(false|true)</param>
        /// <param name="mailPriority">邮件优先级(MailPriority.Low|MailPriority.Normal|MailPriority.High)</param>
        /// <param name="mailEncoding">邮件标题和正文编码(可为 null;为 null时默认System.Text.Encoding.UTF8)</param>
        /// <param name="Async">是否以异步发送邮件(true|false)</param>
        public static void SendMail(string strMailFromAddress, string strMailDisplayName, string strMailFromPwd, string[] arrMailToAddress, string[] arrMailCcAddress, string strSmtpServer, string strSubject, string strBody, string[] arrMailAttachment, bool boolIsBodyHtml, MailPriority mailPriority, Encoding mailEncoding, bool Async)
        {
            #region 参数验证

            //发件人地址不能为空
            if (string.IsNullOrEmpty(strMailFromAddress))
            {
                return;
            }

            //发件人显示名称为空时，默认显示发件人地址
            if (string.IsNullOrEmpty(strMailDisplayName))
            {
                strMailDisplayName = strMailFromAddress;
            }

            //发件人密码不能为空
            if (string.IsNullOrEmpty(strMailFromPwd))
            {
                return;
            }

            //收件人地址不能为空
            if (arrMailToAddress == null)
            {
                return;
            }

            //Smtp服务器地址不能为空
            if (string.IsNullOrEmpty(strSmtpServer))
            {
                return;
            }

            //邮件标题不能为空
            if (string.IsNullOrEmpty(strSubject))
            {
                return;
            }

            //默认邮件编码为 UTF8
            if (mailEncoding == null)
            {
                mailEncoding = System.Text.Encoding.UTF8;
            }

            //默认邮件正文不是HTML格式
            //if (boolIsBodyHtml == null)
            //{
            //    boolIsBodyHtml = false;
            //}           

            //默认邮件优先级为 正常
            //if (mailPriority == null)
            //{
            //    mailPriority = MailPriority.Normal;
            //}

            //默认以异步方式发送邮件
            //if (Async == null)
            //{
            //    Async = true;
            //}
            #endregion

            #region MailMessage

            MailMessage mailMsg = new MailMessage();

            //发件人地址
            mailMsg.From = new MailAddress(strMailFromAddress, strMailDisplayName, mailEncoding);

            //收件人地址
            for (int i = 0; i < arrMailToAddress.Length; i++)
            {
                mailMsg.To.Add(arrMailToAddress[i]);
            }

            //抄送人地址
            if (arrMailCcAddress != null)
            {
                for (int i = 0; i < arrMailCcAddress.Length; i++)
                {
                    mailMsg.CC.Add(arrMailCcAddress[i]);
                }
            }

            //邮件标题
            mailMsg.Subject = strSubject;

            //邮件标题编码 
            mailMsg.SubjectEncoding = mailEncoding;

            //邮件正文
            mailMsg.Body = strBody;

            //邮件正文编码 
            mailMsg.BodyEncoding = mailEncoding;

            //含有附件
            if (arrMailAttachment != null)
            {
                for (int i = 0; i < arrMailAttachment.Length; i++)
                {
                    mailMsg.Attachments.Add(new Attachment(arrMailAttachment[i]));
                }
            }

            //邮件优先级
            mailMsg.Priority = mailPriority;

            //邮件正文是否是HTML格式
            mailMsg.IsBodyHtml = boolIsBodyHtml;

            #endregion

            #region SmtpClient

            SmtpClient smtpClient = new SmtpClient();

            //验证发件人用户名和密码
            smtpClient.Credentials = new System.Net.NetworkCredential(strMailFromAddress, strMailFromPwd);

            //Smtp服务器地址
            smtpClient.Host = strSmtpServer;

            object userState = mailMsg;

            try
            {
                if (Async)
                {
                    //异步发送邮件
                    smtpClient.SendAsync(mailMsg, userState);
                }
                else
                {
                    //同步发送邮件
                    smtpClient.Send(mailMsg);
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //处理异常
                throw ex;
            }

            #endregion

        }
        #endregion

        #region 异/同步发送邮件
        /// <summary>
        /// 异/同步发送邮件
        /// 注：如果是页面调用异步方法，则需要设置页面 Page 的属性 Async="true"
        /// </summary>
        /// <param name="strMailFromAddress">发件人邮箱地址</param>
        /// <param name="strMailFromPwd">发件人邮箱密码</param>
        /// <param name="arrMailToAddress">收件人邮箱地址</param>
        /// <param name="strSmtpServer">Smtp服务器地址("mail.bhinfoLib.com")</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
        public static void SendMail(string strMailFromAddress, string strMailFromPwd, string[] arrMailToAddress, string strSmtpServer, string strSubject, string strBody)
        {
            SendMail(strMailFromAddress, null, strMailFromPwd, arrMailToAddress, null, strSmtpServer, strSubject, strBody, null, false, MailPriority.Normal, System.Text.Encoding.UTF8, true);
        }
        #endregion

        #region 异/同步发送邮件
        /// <summary>
        ///  异/同步发送邮件
        ///  注：1.如果是页面调用异步方法，则需要设置页面 Page 的属性 Async="true"
        ///      2.邮件信息需要在Web.config文件中配置
        ///      <add key="MailFromAddress" value="hehl@bhinfoLib.com"/>
        ///      <add key="MailDisplayName" value="缪新"/>
        ///      <add key="MailFromPwd" value="hehl"/>
        ///      <add key="MailToAddress" value="hehl@bhinfoLib.com;hehl_231@126.com"/>
        ///      <add key="MailCcAddress" value="hehl@bhinfoLib.com;hehl_231@126.com"/>
        ///      <add key="SmtpServer" value="mail.bhinfoLib.com"/>		
        /// </summary>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件正文(可为 空)</param>
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

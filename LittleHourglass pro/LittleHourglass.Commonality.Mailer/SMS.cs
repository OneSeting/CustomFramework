using LittleHourglass.Commonality.BaseExpand;
using LittleHourglass.Logger;
using LittleHourglass.Mailer.Model;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace LittleHourglass.Mailer
{
    public class SMS
    {
        #region Fields
        private static readonly string _accountSid = "";//UniConfiguration.Get("Twilio_AccountSid").IsBlankThen("AC07548d71dd31e80aaefb85bffec591b4");
        private static readonly string _authToken = ""; //UniConfiguration.Get("Twilio_AuthToken").IsBlankThen("b6574716863ebef4859f0ba70935bb19");
        private static readonly string _phoneNumber = "";//UniConfiguration.Get("Twilio_PhoneNumber").IsBlankThen("+12058808092");
        #endregion

        #region Utilities
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="destinationNumber">接受短信号码</param>
        /// <param name="text">文本内容</param>
        /// <returns></returns>
        public static MethodReturnBox SendMessage(string destinationNumber, string text)
        {
            
            var success = true;
            string returnMessage;
            try
            {
                TwilioClient.Init(_accountSid, _authToken);
                var message = MessageResource.Create(
                    body: text,
                    from: new Twilio.Types.PhoneNumber(_phoneNumber),
                    to: new Twilio.Types.PhoneNumber(destinationNumber)
                );
                returnMessage = message.AccountSid;
                LoggerHelper.Info("SendMessage success AccountSid:" + returnMessage);
            }
            catch (Exception e)
            {
                returnMessage = e.Message.ToString();
                success = false;
            }

            return new MethodReturnBox { Message = returnMessage, Success = success };
        }

        /// <summary>
        /// 打电话
        /// </summary>
        /// <param name="destinationNumber">接受电话号码</param>
        /// <param name="text">一个在线文件上面写着配置比如语句之类的参考twilio官网的xml文件估计是要写几种xml模板</param>
        /// <returns></returns>
        public static MethodReturnBox CallUp(string destinationNumber, string docUrl)
        {
            var success = true;
            string returnMessage;
            try
            {
                TwilioClient.Init(_accountSid, _authToken);
                var message = CallResource.Create(
                    url: new Uri(docUrl),
                    //url: new Uri("http://demo.twilio.com/docs/voice.xml"),
                    from: new Twilio.Types.PhoneNumber(_phoneNumber),
                    to: new Twilio.Types.PhoneNumber(destinationNumber)
                );
                returnMessage = message.AccountSid;
                LoggerHelper.Info("CallUp success AccountSid:" + returnMessage);
            }
            catch (Exception e)
            {
                returnMessage = e.Message.ToString();
                success = false;
            }
            return new MethodReturnBox { Message = returnMessage, Success = success };
        }

        /// <summary>
        /// 发送email
        /// </summary>
        /// <param name="mailArchive"></param>
        /// <param name="mailAccount"></param>
        /// <returns></returns>
        public static MethodReturnBox SendEmail(MailArchive mailArchive, MailAccount mailAccount)
        {
            bool state = true;
            string result = string.Empty;
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailAccount.SmtpSenderName, mailAccount.SmtpSenderMail));//SmtpSenderName 发送者的名字,SmtpUsername 发送者的邮箱地址
                message.To.Add(new MailboxAddress(mailArchive.SendToName, mailArchive.SendTo));//SendToName 都是接收者名字,SendTo  接收者的邮箱地址
                if (mailArchive.Cc != null && mailArchive.Cc.Length > 0)
                {
                    for (int i = 0; i < mailArchive.Cc.Length; i++)
                    {
                        message.Cc.Add(new MailboxAddress(mailArchive.Cc[i], mailArchive.Cc[i]));
                    }
                }
                if (mailArchive.Bcc != null && mailArchive.Bcc.Length > 0)
                {
                    for (int i = 0; i < mailArchive.Bcc.Length; i++)
                    {
                        message.Bcc.Add(new MailboxAddress(mailArchive.Bcc[i], mailArchive.Bcc[i]));
                    }
                }
                message.Subject = mailArchive.Title;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = mailArchive.BodyContent;  //BodyContent html邮件内容
                bodyBuilder.TextBody = mailArchive.TextBody;     //text邮件内容
                message.Body = bodyBuilder.ToMessageBody();
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtpClient.Connect(mailAccount.SmtpHost, mailAccount.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);//当与服务器连接后，立即升级为TLS连接   MailKit.Security.SecureSocketOptions.StartTls
                    smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    smtpClient.Authenticate(mailAccount.SmtpUsername, mailAccount.SmtpPlainPassword);
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                    result = "succeed";
                    state = true;
                }
            }
            catch (Exception e)
            {
                result = "failed";
                state = false;
            }
            return new MethodReturnBox { Success = state, Message = result };
        }
        #endregion
    }
}

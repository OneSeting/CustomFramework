using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Mailer.Model
{
    public class MailAccount
    {
        /// <summary>
        /// 发送邮件的host地址
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        public string SmtpSenderName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
		public string SmtpUsername { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
		public string SmtpPlainPassword { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
		public int SmtpPort { get; set; }

        /// <summary>
        /// 发送者邮箱
        /// </summary>
		public string SmtpSenderMail { get; set; }

    }

}

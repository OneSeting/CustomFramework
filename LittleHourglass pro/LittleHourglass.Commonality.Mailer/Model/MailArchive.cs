using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Mailer.Model
{
    public class MailArchive
    {
        /// <summary>
        /// 标题
        /// </summary>
		public string Title { get; set; }

        /// <summary>
        /// 邮件发送给谁
        /// </summary>
		public string SendTo { get; set; }

        /// <summary>
        /// 邮件的内容（可以使用html） 
        /// </summary>
        public string BodyContent { get; set; }

        /// <summary>
        /// 好像可以传null只穿html就可以了
        /// </summary>
		public string TextBody { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
		public string SendToName { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string[] Cc { get; set; }

        /// <summary>
        /// 秘密抄送
        /// </summary>
        public string[] Bcc { get; set; }

    }
}

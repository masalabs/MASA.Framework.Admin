using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Extensions.Tools.Emails.Model
{
    public class EmailHostSetting
    {
        /// <summary>
        /// 发送人
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 客户端授权码(可存在配置文件中)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string Host { get; set; }
    }
}

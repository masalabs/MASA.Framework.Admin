using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MASA.Framework.Extensions.Tools.Emails.Model
{
    public class EmailParameter
    {
        /// <summary>
        /// 收件人地址(多人)
        /// </summary>
        public string[] RecipientArry { get; set; } 

        /// <summary>
        /// 抄送地址(多人)
        /// </summary>
        public string[] MailCcArray { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool IsbodyHtml { get; set; }

        /// <summary>
        /// 接收文件
        /// </summary>
        public List<IFormFile> Files { get; set; } = new();
    }
}

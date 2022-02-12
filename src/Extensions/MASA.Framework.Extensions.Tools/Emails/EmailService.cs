using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MASA.Framework.Extensions.Tools.Emails.Model;
using MASA.Utils.Configuration.Json;
using Microsoft.AspNetCore.Http;
using MimeKit;
using NPOI.SS.UserModel;

namespace MASA.Framework.Extensions.Tools
{
    public class EmailService
    {
        private static EmailHostSetting HostSetting { get; set; }

        static EmailService()
        {
            HostSetting = AppSettings.GetModel<EmailHostSetting>("EmailHostConfig");
            
            if (HostSetting == null)
            {
                throw new ArgumentNullException("emailHostConfig is null");
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public static bool Send(EmailParameter mails)
        {
            //截取发件人邮箱地址从而判断Smtp的值
            //string[] sArray = mails.FromPerson.Split(new char[2] { '@', '.' });

            //将发件人邮箱带入MailAddress中初始化
            MailAddress mailAddress = new MailAddress(HostSetting.FromPerson);
            //创建Email的Message对象
            MailMessage mailMessage = new MailMessage();

            //判断收件人数组中是否有数据
            if (mails.RecipientArry is not null)
            {
                //循环添加收件人地址
                foreach (var item in mails.RecipientArry)
                {
                    if (!string.IsNullOrEmpty(item))
                        mailMessage.To.Add(item.ToString());
                }
            }

            //判断抄送地址数组是否有数据
            if (mails.MailCcArray is not null)
            {
                //循环添加抄送地址
                foreach (var item in mails.MailCcArray)
                {
                    if (!string.IsNullOrEmpty(item))
                        mailMessage.To.Add(item.ToString());
                }
            }
            //发件人邮箱
            mailMessage.From = mailAddress;
            //标题
            mailMessage.Subject = mails.Title;
            //编码
            mailMessage.SubjectEncoding = Encoding.UTF8;
            //正文
            mailMessage.Body = mails.Body;
            //正文编码
            mailMessage.BodyEncoding = Encoding.Default;
            //邮件优先级
            mailMessage.Priority = MailPriority.High;
            //正文是否是html格式
            mailMessage.IsBodyHtml = mails.IsbodyHtml;
            //取得Web根目录和内容根目录的物理路径
            string webRootPath = string.Empty;
            //添加附件
            foreach (IFormFile item in mails.Files)
            {
                //路径拼接
                //webRootPath = _hostingEnvironment.WebRootPath + "\\" + "upload-file" + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetFileNameWithoutExtension(item.FileName) + Path.GetExtension(item.FileName).ToLower();
                //创建文件流
                //using (var FileStream = new FileStream(webRootPath, FileMode.Create))
                //{
                //拷贝文件流
                //await item.CopyToAsync(FileStream);
                //释放缓存
                //FileStream.Flush();
                //}
                //再根据路径打开文件，得到文件流
                //FileStream stream = new FileStream(webRootPath, FileMode.Open);

                //添加至附件中
                //mailMessage.Attachments.Add(new Attachment(stream, item.FileName));
                mailMessage.Attachments.Add(new Attachment(item.OpenReadStream(), item.FileName));
            };

            //实例化一个Smtp客户端
            SmtpClient smtp = new SmtpClient();
            //将发件人的邮件地址和客户端授权码带入以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(HostSetting.FromPerson, HostSetting.Code);
            //指定SMTP邮件服务器
            smtp.Host = HostSetting.Host;

            //邮件发送到SMTP服务器
            smtp.Send(mailMessage);
            return true;
        }
    }

}

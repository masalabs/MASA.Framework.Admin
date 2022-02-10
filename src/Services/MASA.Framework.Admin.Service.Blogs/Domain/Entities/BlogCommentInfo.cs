using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 评论信息
    /// </summary>
    public class BlogCommentInfo : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }


        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }
    }
}

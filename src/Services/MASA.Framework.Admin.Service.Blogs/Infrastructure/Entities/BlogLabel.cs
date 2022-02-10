using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Entities
{
    /// <summary>
    /// 文章标签
    /// </summary>
    public class BlogLabel : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string LableName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Entities
{
    /// <summary>
    /// 附件信息表
    /// </summary>
    public class BlogEnclosureInfo : EntityBase
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
        /// 文件上传地址
        /// </summary>
        public string Address { get; set; }
    }
}

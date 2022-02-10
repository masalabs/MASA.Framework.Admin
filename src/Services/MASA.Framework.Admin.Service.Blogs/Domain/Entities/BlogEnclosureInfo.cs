using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 附件信息表
    /// </summary>
    public class BlogEnclosureInfo : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 文件上传地址
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Address { get; set; } = string.Empty;
    }
}

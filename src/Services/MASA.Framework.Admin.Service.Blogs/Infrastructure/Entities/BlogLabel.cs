using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LableName { get; set; }
    }
}

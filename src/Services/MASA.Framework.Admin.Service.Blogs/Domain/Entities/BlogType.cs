using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public class BlogType : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; } = string.Empty;
    }
}

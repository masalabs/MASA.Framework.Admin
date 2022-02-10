using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 广告图片
    /// </summary>
    public class BlogAdvertisingPictures : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Pic { get; set; }
    }
}

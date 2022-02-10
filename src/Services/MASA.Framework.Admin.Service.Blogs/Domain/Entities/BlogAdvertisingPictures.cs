using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Pic { get; set; } = string.Empty;

        /// <summary>
        /// 类型：1 首页 2文章列表
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public short Type { get; set; }

        /// <summary>
        /// 位置：1 列表头部
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public short Location { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [DefaultValue(1)]

        public int Sort { get; set; }
    }
}

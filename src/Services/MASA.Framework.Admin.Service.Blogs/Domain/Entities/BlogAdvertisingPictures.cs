using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

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
        /// 类型：1 首页 2首页右下 3详情 4详情右下
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public BlogAdvertisingPicturesTypes Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [DefaultValue(1)]

        public int Sort { get; set; }

        /// <summary>
        /// 状态：true启用 false停用
        /// </summary>
        public bool Status { get; set; }
    }
}

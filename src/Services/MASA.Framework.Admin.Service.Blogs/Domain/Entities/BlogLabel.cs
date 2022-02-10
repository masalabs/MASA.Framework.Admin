using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
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
        public string LabelName { get; set; } = string.Empty;
    }
}

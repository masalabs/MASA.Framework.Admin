using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 点赞记录
    /// </summary>
    public class BlogApprovedRecord : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }
        /// <summary>
        /// 博客ID
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid BlogInfoId { get; set; }
    }
}

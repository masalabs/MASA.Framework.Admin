using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    public class BlogReport : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 举报标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 举报链接
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Connect { get; set; } = string.Empty;

        /// <summary>
        /// 文章编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 举报详细信息
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Detail { get; set; } = string.Empty;

        /// <summary>
        /// 举报理由
        /// </summary>
        [Required]
        public ReasonTypes Reason { get; set; }
    }
}

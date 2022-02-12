using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    /// <summary>
    /// 文章信息表
    /// </summary>
    [Description("文章信息")]
    public class BlogInfo : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [StringLength(36)]
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 文章状态
        /// </summary>
        public StateTypes State { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid TypeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 访问数量
        /// </summary>
        [Required]
        public int Visits { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        [Required]
        public int CommentCount { get; set; }

        /// <summary>
        /// 是否仅自己可见
        /// </summary>
        [Required]
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 点赞数量
        /// </summary>
        [Required]
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        [Required]
        public DateTime ReleaseTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 下架原因
        /// </summary>
        public string WithdrawReason { get; set; }
    }
}
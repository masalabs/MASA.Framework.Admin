using MASA.BuildingBlocks.Data.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    public class EntityBase : ISoftDelete
    {
        /// <summary>
        /// IsDeleted
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// DeleterUserId
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid DeleterUserId { get; set; }

        /// <summary>
        /// DeletionTime
        /// </summary>
        [Required]
        public DateTime DeletionTime { get; set; } = DateTime.Now;

        /// <summary>
        /// CreationTime
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("getdate()")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// CreatorUserId
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// LastModificationTime
        /// </summary>
        [Required]
        [DefaultValue("getdate()")]
        public DateTime LastModificationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// LastModifierUserId
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid LastModifierUserId { get; set; }
    }
}

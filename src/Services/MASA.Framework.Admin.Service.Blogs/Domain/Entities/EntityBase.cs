using MASA.BuildingBlocks.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime DeletionTime { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTime { get; set; }

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
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        /// LastModifierUserId
        /// </summary>
        [Required]
        [StringLength(36)]
        public Guid LastModifierUserId { get; set; }
    }
}

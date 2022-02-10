using MASA.BuildingBlocks.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Entities
{
    public class EntityBase : ISoftDelete
    {
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// DeleterUserId
        /// </summary>
        public Guid DeleterUserId { get; set; }

        /// <summary>
        /// DeletionTime
        /// </summary>
        public DateTime DeletionTime { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// CreatorUserId
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        /// LastModifierUserId
        /// </summary>
        public Guid LastModifierUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Blogs.Domain.Entities
{
    public class EntityBase
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

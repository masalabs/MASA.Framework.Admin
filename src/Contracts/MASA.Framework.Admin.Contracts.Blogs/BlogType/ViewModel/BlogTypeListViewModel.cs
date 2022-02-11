using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogTypeListViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>

        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [StringLength(50)]
        public string TypeName { get; set; }
        
        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}

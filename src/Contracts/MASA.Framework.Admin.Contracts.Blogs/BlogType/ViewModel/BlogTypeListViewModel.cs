using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Blogs.Model.BlogType.ViewModel
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
    }
}

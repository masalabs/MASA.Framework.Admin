namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogTypePagingViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}

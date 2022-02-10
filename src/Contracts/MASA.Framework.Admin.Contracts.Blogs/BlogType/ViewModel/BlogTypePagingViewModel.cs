namespace MASA.Framework.Admin.Contracts.Blogs.BlogType.Options
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
        public string TypeName { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime LastModificationTime { get; set; } = DateTime.Now;
    }
}

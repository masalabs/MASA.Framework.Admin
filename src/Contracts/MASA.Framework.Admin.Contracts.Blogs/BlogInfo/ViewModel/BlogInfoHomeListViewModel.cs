namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogInfoHomeListViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
        public DateTime ReleaseTime { get; set; }
        public int ApprovedCount { get; set; }
        public int CommentCount { get; set; }
        public int Visits { get; set; }
        public Guid CreatorUserId { get; set; }
    }
}

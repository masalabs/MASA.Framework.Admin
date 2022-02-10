namespace MASA.Framework.Admin.Contracts.Blogs.BlogCommentInfo.ViewModel
{
    public class BlogCommentInfoListViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
    }
}

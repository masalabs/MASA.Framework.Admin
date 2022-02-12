namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class AddCommentModel
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid BlogInfoId { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 回复id
        /// </summary>
        public Guid ReplyId { get; set; } = Guid.Empty;
    }
}

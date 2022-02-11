namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogApprovedRecordModel
    {
        /// <summary>
        /// 博文Id
        /// </summary>
        public Guid BlogId { get; set; }

        /// <summary>
        /// 是否点赞 true：点赞，false：取消点赞
        /// </summary>
        public bool IsApproved { get; set; } = true;

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
    }
}

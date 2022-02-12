namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class WithdrawBlogArticleModel
    {
        public Guid Id { get; set; }

        [Required]
        [NonDefault]
        public ReasonTypes ReasonType { get; set; }

        public string? ReasonDetail { get; set; }
    }
}

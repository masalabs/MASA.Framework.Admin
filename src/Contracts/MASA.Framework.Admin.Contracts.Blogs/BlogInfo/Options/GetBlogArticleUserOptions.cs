namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class GetBlogArticleUserOptions : PagingOptions
    {
        public Guid Author { get; set; }

        public string Title { get; set; }
    }
}

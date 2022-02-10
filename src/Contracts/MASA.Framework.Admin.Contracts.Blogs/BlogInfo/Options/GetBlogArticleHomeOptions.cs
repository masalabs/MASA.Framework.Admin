namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class GetBlogArticleHomeOptions : PagingOptions
    {
        public string KeyWords { get; set; }

        public Guid? TypeId { get; set; }
    }
}

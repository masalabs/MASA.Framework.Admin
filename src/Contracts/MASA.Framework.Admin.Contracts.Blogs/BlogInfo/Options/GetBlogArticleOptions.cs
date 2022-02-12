namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class GetBlogArticleOptions : PagingOptions
    {
        public string Title { get; set; }

        public StateTypes? State { get; set; }
        
        public DateTime? ReleaseStartTime { get; set; }
        
        public DateTime? ReleaseEndTime { get; set; }

        public Guid? TypeId { get; set; }
        
        // TODO: not implement
        public string Author { get; set; }
    }
}

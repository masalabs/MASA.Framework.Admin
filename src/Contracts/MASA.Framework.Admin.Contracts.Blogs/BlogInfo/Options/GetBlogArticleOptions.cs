

namespace MASA.Framework.Admin.Contracts.Blogs.BlogInfo.Options
{
    public class GetBlogArticleOptions : PagingOptions
    {
        public string Title { get; set; }

        public int? State { get; set; }
        
        public DateTime ReleaseStartTime { get; set; }
        
        public DateTime ReleaseEndTime { get; set; }
        
        public string Author { get; set; }
    }
}

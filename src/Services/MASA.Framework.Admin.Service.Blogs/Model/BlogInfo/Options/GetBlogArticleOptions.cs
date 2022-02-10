using MASA.Framework.Admin.Service.Blogs.Infrastructure.Enum;

namespace MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options
{
    public class GetBlogArticleOptions : PagingOptions
    {
        public string Title { get; set; }

        public StateTypes? State { get; set; }
        
        public DateTime ReleaseStartTime { get; set; }
        
        public DateTime ReleaseEndTime { get; set; }
        
        public string Author { get; set; }
    }
}

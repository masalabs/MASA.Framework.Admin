using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class GetBlogAdvertisingPicturesOption : PagingOptions
    {

        public BlogAdvertisingPicturesTypes? Type { get; set; }

    }
}

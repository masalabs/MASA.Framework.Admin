using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Querys
{
    public record class GetBlogAdvertisingPicturesQuery : Query<PageResult<BlogAdvertisingPicturesListViewModel>>
    {
        public GetBlogAdvertisingPicturesQuery()
        {

        }

        public GetBlogAdvertisingPicturesQuery(GetBlogAdvertisingPicturesOption request)
        {
            this.Request = request;
        }

        public GetBlogAdvertisingPicturesOption Request { get; set; }

        public override PageResult<BlogAdvertisingPicturesListViewModel> Result { get; set; }
    }
}

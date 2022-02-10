using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys
{
    public record class GetBlogTypePagingQuery : Query<PageResult<BlogTypePagingViewModel>>
    {
        public GetBlogTypePagingQuery()
        {

        }

        public GetBlogTypePagingQuery(GetBlogTypePagingOption request)
        {
            this.Request = request;
        }

        public GetBlogTypePagingOption Request { get; set; }

        public override PageResult<BlogTypePagingViewModel> Result { get; set; }
    }
}

using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleQuery : Query<PageResult<BlogInfoListViewModel>>
    {
        public GetBlogArticleOptions Options { get; set; }

        public override PageResult<BlogInfoListViewModel> Result { get; set; } = default!;
    }
}

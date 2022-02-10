using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleQuery : Query<BlogInfo>
    {
        public GetBlogArticleOptions Options { get; set; }

        public override BlogInfo Result { get; set; } = default!;
    }
}

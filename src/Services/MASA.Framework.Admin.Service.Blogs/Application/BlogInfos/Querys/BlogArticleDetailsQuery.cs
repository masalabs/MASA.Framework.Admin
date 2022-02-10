using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleDetailsQuery : Query<BlogInfoListViewModel>
    {
        public Guid Id { get; set; }

        public override BlogInfoListViewModel Result { get; set; } = default!;
    }
}

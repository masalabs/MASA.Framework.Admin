namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public record class BlogArticleDetailsQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<BlogInfoListViewModel>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public override BlogInfoListViewModel Result { get; set; } = default!;
    }
}

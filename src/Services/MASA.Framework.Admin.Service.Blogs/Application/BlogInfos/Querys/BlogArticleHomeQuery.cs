namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleHomeQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PageResult<BlogInfoHomeListViewModel>>
    {
        public GetBlogArticleHomeOptions Options { get; set; }

        public override PageResult<BlogInfoHomeListViewModel> Result { get; set; } = default!;
    }
}

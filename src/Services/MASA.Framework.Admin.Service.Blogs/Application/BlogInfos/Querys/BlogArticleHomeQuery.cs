namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleHomeQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PagingResult<BlogInfoHomeListViewModel>>
    {
        public GetBlogArticleHomeOptions Options { get; set; }

        public override PagingResult<BlogInfoHomeListViewModel> Result { get; set; } = default!;
    }
}

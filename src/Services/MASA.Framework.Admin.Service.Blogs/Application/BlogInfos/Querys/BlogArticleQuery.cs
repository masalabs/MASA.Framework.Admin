namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PagingResult<BlogInfoListViewModel>>
    {
        public GetBlogArticleOptions Options { get; set; }

        public override PagingResult<BlogInfoListViewModel> Result { get; set; } = default!;
    }
}
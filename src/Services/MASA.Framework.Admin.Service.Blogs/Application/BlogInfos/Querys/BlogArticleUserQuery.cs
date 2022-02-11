namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleUserQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PagingResult<BlogInfoListViewModel>>
    {
        public GetBlogArticleUserOptions Options { get; set; }

        public override PagingResult<BlogInfoListViewModel> Result { get; set; } = default!;
    }
}

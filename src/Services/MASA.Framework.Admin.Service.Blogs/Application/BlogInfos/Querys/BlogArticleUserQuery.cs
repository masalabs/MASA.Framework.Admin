namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleUserQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PageResult<BlogInfoListViewModel>>
    {
        public GetBlogArticleUserOptions Options { get; set; }

        public override PageResult<BlogInfoListViewModel> Result { get; set; } = default!;
    }
}

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys
{
    public record class BlogArticleQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PageResult<BlogInfoListViewModel>>
    {
        public GetBlogArticleOptions Options { get; set; }

        public override PageResult<BlogInfoListViewModel> Result { get; set; } = default!;
    }
}
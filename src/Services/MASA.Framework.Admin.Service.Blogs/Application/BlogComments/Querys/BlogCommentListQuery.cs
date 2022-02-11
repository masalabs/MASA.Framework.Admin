namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Querys
{
    public record class BlogCommentListQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PageResult<BlogCommentInfoListViewModel>>
    {
        public GetBlogCommentInfoOptions Options { get; set; }

        public override PageResult<BlogCommentInfoListViewModel> Result { get; set; } = default!;
    }
}

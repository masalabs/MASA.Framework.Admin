namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Querys
{
    public record class BlogCommentListQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PagingResult<BlogCommentInfoListViewModel>>
    {
        public GetBlogCommentInfoOptions Options { get; set; }

        public override PagingResult<BlogCommentInfoListViewModel> Result { get; set; } = default!;
    }
}

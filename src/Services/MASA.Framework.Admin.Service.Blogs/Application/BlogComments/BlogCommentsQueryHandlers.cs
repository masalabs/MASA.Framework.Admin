using MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Querys;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments
{
    public class BlogCommentsQueryHandlers
    {
        private readonly IBlogCommentInfoRepository _commentInfoRepository;

        public BlogCommentsQueryHandlers(
            IBlogCommentInfoRepository commentInfoRepository)
        {
            _commentInfoRepository = commentInfoRepository;
        }

        [EventHandler]
        public async Task GetBlogComments(BlogCommentListQuery query)
        {
            var comments = await _commentInfoRepository.GetBlogComments(query.Options);

            query.Result = comments;
        }
    }
}

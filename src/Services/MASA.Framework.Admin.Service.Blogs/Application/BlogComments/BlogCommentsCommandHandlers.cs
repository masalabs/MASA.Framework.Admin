using MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Commands;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments
{
    public class BlogCommentsCommandHandlers
    {
        private readonly IBlogCommentInfoRepository _commentInfoRepository;
        private readonly IBlogArticleRepository _blogArticleRepository;
        public BlogCommentsCommandHandlers(
            IBlogArticleRepository blogArticleRepository,
            IBlogCommentInfoRepository commentInfoRepository)
        {
            _blogArticleRepository = blogArticleRepository;
            _commentInfoRepository = commentInfoRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogCommentCommand command)
        {
            await _commentInfoRepository.CreateAsync(new()
            { 
                BlogInfoId = command.Request.BlogInfoId,
                CreatorUserId = command.Request.CreatorUserId,
                IpAddress = command.Request.IpAddress,
                CommentContent = command.Request.CommentContent,
                QQ = command.Request.QQ,
                TypeId = command.Request.TypeId,
                ReplyId = command.Request.ReplyId
            });

            await _blogArticleRepository.AddCommentCount(command.Request.BlogInfoId, true);
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogCommentCommand command)
        {
            var blogInfoId = await _commentInfoRepository.RemoveAsync(command.Request.Id);

            await _blogArticleRepository.AddCommentCount(blogInfoId, false);
        }
    }
}

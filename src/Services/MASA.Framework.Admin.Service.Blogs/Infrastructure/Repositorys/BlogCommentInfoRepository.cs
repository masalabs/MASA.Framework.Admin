namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogCommentInfoRepository: IBlogCommentInfoRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogCommentInfoRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        public async Task<BlogCommentInfo> CreateAsync(BlogCommentInfo entity)
        {
            var result = await _blogDbContext.BlogCommentInfoes.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            var comment = await _blogDbContext.BlogCommentInfoes.FindAsync(id);
            if (comment is null)
                return Guid.Empty;

            comment.IsDeleted = true;
            comment.DeletionTime = DateTime.UtcNow;

            _blogDbContext.BlogCommentInfoes.Update(comment);
            await _blogDbContext.SaveChangesAsync();

            return comment.BlogInfoId;
        }

        public async Task<PagingResult<BlogCommentInfoListViewModel>> GetBlogComments(
            GetBlogCommentInfoOptions options)
        {
            return await _blogDbContext.BlogCommentInfoes
                .Where(x => x.BlogInfoId.Equals(options.BlogInfoId) && x.IsShow && !x.IsDeleted)
                .Select(x => new BlogCommentInfoListViewModel
                {
                    BlogInfoId = x.BlogInfoId,
                    CommentContent = x.CommentContent,
                    CreatorUserId = x.CreatorUserId,
                    Id = x.Id,
                    IpAddress = x.IpAddress,
                    QQ = x.QQ,
                    TypeId = x.TypeId,
                    CreationTime = x.CreationTime,
                })
                .OrderByDescending(x => x.CreationTime)
                .PagingAsync(options.PageIndex, options.PageSize);
        }
    }
}

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
            _blogDbContext.Database.CurrentTransaction?.Commit();

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
            _blogDbContext.Database.CurrentTransaction?.Commit();

            return comment.BlogInfoId;
        }

        public async Task<PagingResult<BlogCommentInfoListViewModel>> GetBlogComments(
            GetBlogCommentInfoOptions options)
        {
            return await (from x in _blogDbContext.BlogCommentInfoes
                          join y in _blogDbContext.BlogCommentInfoes
                          on new { ReplyId = x.Id, x.IsDeleted } equals new { y.ReplyId, y.IsDeleted }
                          into reply
                          from r in reply.DefaultIfEmpty()
                          where x.BlogInfoId.Equals(options.BlogInfoId) && x.IsShow && x.ReplyId == Guid.Empty && !x.IsDeleted
                          orderby x.CreationTime descending
                          select new BlogCommentInfoListViewModel
                          {
                              BlogInfoId = x.BlogInfoId,
                              CommentContent = x.CommentContent,
                              CreatorUserId = x.CreatorUserId,
                              Id = x.Id,
                              IpAddress = x.IpAddress,
                              QQ = x.QQ,
                              TypeId = x.TypeId,
                              CreationTime = x.CreationTime,
                              ReplyInfo = r == null ? new() : new()
                              {
                                  BlogInfoId = r.BlogInfoId,
                                  CommentContent = r.CommentContent,
                                  CreatorUserId = r.CreatorUserId,
                                  Id = r.Id,
                                  IpAddress = r.IpAddress,
                                  QQ = r.QQ,
                                  TypeId = r.TypeId,
                                  CreationTime = r.CreationTime
                              }
                          }).PagingAsync(options.PageIndex, options.PageSize);
        }
    }
}

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogApprovedRecordRepository : IBlogApprovedRecordRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogApprovedRecordRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        public async Task AddBlogApprovedRecord(BlogApprovedRecordModel model)
        {
            var blogInfo = await _blogDbContext.BlogInfoes.FindAsync(model.BlogId);
            if (blogInfo is null)
                throw new Exception("博文不存在!");

            var record = await _blogDbContext.BlogApprovedRecords.FirstOrDefaultAsync(
                x => x.CreatorUserId == model.UserId && x.BlogInfoId == model.BlogId);

            if (record is null)
                await _blogDbContext.BlogApprovedRecords.AddAsync(new()
                {
                    BlogInfoId = model.BlogId,
                    CreatorUserId = model.UserId
                });
            else
            {
                if ((model.IsApproved && !record.IsDeleted) ||
                    (!model.IsApproved && record.IsDeleted))
                    throw new Exception("请勿重复操作!");

                record.IsDeleted = !model.IsApproved;
                _blogDbContext.BlogApprovedRecords.Update(record);
            }

            blogInfo.ApprovedCount = model.IsApproved ?
            blogInfo.ApprovedCount + 1 : blogInfo.ApprovedCount - 1;
            if (blogInfo.ApprovedCount >= 0)
                _blogDbContext.BlogInfoes.Update(blogInfo);

            await _blogDbContext.SaveChangesAsync();
            _blogDbContext.Database.CurrentTransaction?.Commit();
        }
    }
}

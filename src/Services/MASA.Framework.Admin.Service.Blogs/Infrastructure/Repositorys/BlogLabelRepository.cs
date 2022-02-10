namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogLabelRepository: IBlogLabelRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogLabelRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        /// <summary>
        /// 批量新增标签
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public async Task<List<Guid>> CreateBatchAsync(IEnumerable<string> labels)
        {
            if (labels is null || !labels.Any())
                return null;

            var dbExists = await _blogDbContext.BlogLabels.Where(
                x => labels.Contains(x.LabelName)).ToListAsync();

            var needAddLabel = dbExists is null ? labels :
                labels.Except(dbExists.Select(x => x.LabelName));

            var result = dbExists is null ? new() : dbExists.Select(x => x.Id).ToList();
            if (needAddLabel.Any())
            {
                var res = needAddLabel.Select(x => new Domain.Entities.BlogLabel { LabelName = x });
                await _blogDbContext.BlogLabels.AddRangeAsync(res);
                result.Union(res.Select(x => x.Id));
            }

            await _blogDbContext.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// 批量下创建标签与博客关联关系
        /// </summary>
        /// <param name="relationships"></param>
        /// <returns></returns>
        public async Task CreateBlogLabelRelationBatchAsync(
            IEnumerable<BlogLabelRelationship> relationships)
        {
            if (relationships is null || !relationships.Any())
                return;

            await _blogDbContext.BlogLabelRelationships.AddRangeAsync(relationships);

            await _blogDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除创建标签与博客关联关系
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteBlogLabelRelationBatchAsync(IEnumerable<Guid> ids)
        {
            if (ids is null || !ids.Any())
                return;

            var list = await _blogDbContext.BlogLabelRelationships.Where(x => ids.Contains(x.Id)).ToListAsync();
            if (list is null)
                return;

            list.ForEach(x =>
            {
                x.IsDeleted = true;
                x.DeletionTime = DateTime.UtcNow;
            });

            _blogDbContext.BlogLabelRelationships.UpdateRange(list);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task<List<BlogLabelRelationsViewModel>> GetRelationsByBlog(Guid blogId)
        { 
            return await (from x in _blogDbContext.BlogLabelRelationships
                          join y in _blogDbContext.BlogLabels
                          on x.BlogLabelId equals y.Id
                          where x.BlogInfoId == blogId
                          orderby x.CreationTime ascending
                          select new BlogLabelRelationsViewModel
                          { 
                              RelationId = x.Id,
                              LabelId = y.Id,
                              LabelName = y.LabelName
                          }).ToListAsync();
        }
    }
}

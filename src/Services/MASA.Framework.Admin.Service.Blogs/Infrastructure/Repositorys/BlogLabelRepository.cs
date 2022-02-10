using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using System.Linq;

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
        public async Task<List<Guid>> CreateBatchAsync(List<string> labels)
        {
            if (labels is null || labels.Count == 0)
                return null;

            var dbExists = _blogDbContext.BlogLabels.Where(
                x => labels.Contains(x.LabelName)).ToList();

            var needAddLabel = dbExists is null ? labels :
                labels.Except(dbExists.Select(x => x.LabelName));

            var result = dbExists is null ? new() : dbExists.Select(x => x.Id).ToList();
            if (needAddLabel.Any())
            {
                var res = needAddLabel.Select(x => new BlogLabel { LabelName = x });
                await _blogDbContext.BlogLabels.AddRangeAsync(res);
                result.Union(res.Select(x => x.Id));
            }

            await _blogDbContext.SaveChangesAsync();

            return result;
        }
    }
}

using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogReportRepository : IBlogReportRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogReportRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        /// <summary>
        /// 举报列表
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<PagingResult<BlogReportListViewModel>> GetListAsync(GetBlogReportOptions options)
        {
            var query = from blogReport in _blogDbContext.BlogReports
                join blogInfo in _blogDbContext.BlogInfoes on blogReport.BlogInfoId equals blogInfo.Id into leftBlogInfo
                from blogInfo in leftBlogInfo.DefaultIfEmpty()
                select new BlogReportListViewModel()
                {
                    Id = blogReport.Id,
                    Title = blogReport.Title,
                    BlogInfoId = blogInfo.Id,
                    Detail = blogReport.Detail,
                    Reason = blogReport.Reason,
                    Connect = blogReport.Connect,
                    CreationTime = blogInfo.CreationTime,
                    Handled = blogReport.Handled
                };

            return await query.OrderByDescending(x => x.CreationTime)
                .PagingAsync(options.PageIndex, options.PageSize);
        }

        /// <summary>
        /// 举报
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BlogReport> CreateAsync(BlogReport model)
        {
            var result = await _blogDbContext.BlogReports.AddAsync(model);

            await _blogDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task HandleAsync(Guid id)
        {
            var result = await _blogDbContext.BlogReports.FirstOrDefaultAsync(b => b.Id == id);
            if (result == null)
            {
                // TODO:
                return;
            }

            result.Handled = true;

            _blogDbContext.Update(result);

            await _blogDbContext.SaveChangesAsync();
        }

        public async Task HandleByArticleId(Guid id)
        {
            var reports = await _blogDbContext.BlogReports
                .Where(b => b.Handled == false && b.BlogInfoId == id)
                .ToListAsync();
            
            reports.ForEach(r => r.Handled = true);
            
            _blogDbContext.UpdateRange(reports);
        }
    }
}
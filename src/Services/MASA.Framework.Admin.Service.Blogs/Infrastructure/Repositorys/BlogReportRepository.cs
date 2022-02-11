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
                            BlogInfoId = blogInfo.Id,
                            BlogInfoName = blogInfo.Title,
                            Detail = blogReport.Detail,
                            Reason = blogReport.Reason,
                            CreationTime = blogInfo.CreationTime,
                        };

            var pageResult = await query.OrderBy(x => x.CreationTime).PagingAsync(options.PageIndex, options.PageSize);

            return pageResult;
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
    }
}

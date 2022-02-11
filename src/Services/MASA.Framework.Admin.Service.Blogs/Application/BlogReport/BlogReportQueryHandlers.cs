using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Querys;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport
{
    public class BlogReportQueryHandlers
    {
        private readonly IBlogReportRepository _blogReportRepository;

        public BlogReportQueryHandlers(IBlogReportRepository blogReportRepository)
        {
            _blogReportRepository = blogReportRepository;
        }

        [EventHandler]
        public async Task BlogReportListQueryAsync(BlogReportListQuery query)
        {
            var blogArticle = await _blogReportRepository.GetListAsync(query.Options);

            query.Result = blogArticle;
        }
    }
}

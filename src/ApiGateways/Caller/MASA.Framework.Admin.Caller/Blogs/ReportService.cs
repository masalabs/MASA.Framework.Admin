using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;

namespace MASA.Framework.Admin.Caller.Blogs
{
    public class ReportService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public ReportService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task<PagingResult<BlogReportListViewModel>> GetList(GetBlogReportOptions options)
        {
            return await _callerProviderProvider.PostAsync<GetBlogReportOptions, PagingResult<BlogReportListViewModel>>(
                "/api/blog-report/getList", options);
        }

        public async Task CreateAsync(CreateBlogReportModel options)
        {
            await _callerProviderProvider.PostAsync("/api/blog-report/create", options);
        }

        public async Task IgnoreAsync(IgnoreBlogReportModel model)
        {
            await _callerProviderProvider.PostAsync("/api/blog-report/ignore", model);
        }

        public async Task AgreeAsync(AgreeBlogReportModel model)
        {
            await _callerProviderProvider.PostAsync("/api/blog-report/agree", model);
        }
    }
}

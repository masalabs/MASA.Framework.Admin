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

        public Task<PagingResult<BlogReportListViewModel>> GetList(GetBlogReportOptions options)
        {
            return _callerProviderProvider.PostAsync<GetBlogReportOptions, PagingResult<BlogReportListViewModel>>(
                "/api/blog-report/getList", options);
        }
    }
}


using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;

namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogReportRepository
    {
        Task<PagingResult<BlogReportListViewModel>> GetListAsync(GetBlogReportOptions options);

        Task<BlogReport> CreateAsync(BlogReport model);

        Task HandleAsync(Guid id);

        Task HandleByArticleId(Guid id);
    }
}

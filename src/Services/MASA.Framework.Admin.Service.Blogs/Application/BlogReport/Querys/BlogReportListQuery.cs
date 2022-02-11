using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Querys
{
    public record class BlogReportListQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PagingResult<BlogReportListViewModel>>
    {
        public GetBlogReportOptions Options { get; set; }

        public override PagingResult<BlogReportListViewModel> Result { get; set; } = default!;
    }
}

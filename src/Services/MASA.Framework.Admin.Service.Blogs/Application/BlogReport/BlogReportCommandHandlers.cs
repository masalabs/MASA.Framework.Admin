using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport
{
    public class BlogReportCommandHandlers
    {
        private readonly IBlogReportRepository _blogReportRepository;

        public BlogReportCommandHandlers(IBlogReportRepository blogReportRepository)
        {
            _blogReportRepository = blogReportRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogReportCommand command)
        {
            var blogReport = new Domain.Entities.BlogReport
            {
                Title = command.Request.Title,
                Connect = command.Request.Connect,
                BlogInfoId = command.Request.BlogInfoId,
                Detail = command.Request.Detail,
                Reason = command.Request.Reason,
                Handled = false,
                CreatorUserId = command.Request.CreatorUserId,
                LastModifierUserId = command.Request.CreatorUserId
            };

            await _blogReportRepository.CreateAsync(blogReport);
        }
    }
}

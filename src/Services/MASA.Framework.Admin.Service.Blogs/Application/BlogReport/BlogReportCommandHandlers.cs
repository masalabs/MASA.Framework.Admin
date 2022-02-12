using MASA.BuildingBlocks.Data.UoW;
using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport
{
    public class BlogReportCommandHandlers
    {
        private readonly IBlogReportRepository _blogReportRepository;
        private readonly IBlogArticleRepository _blogArticleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogReportCommandHandlers(
            IBlogReportRepository blogReportRepository,
            IBlogArticleRepository blogArticleRepository,
            IUnitOfWork unitOfWork)
        {
            _blogReportRepository = blogReportRepository;
            _blogArticleRepository = blogArticleRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.CommitAsync();
        }

        [EventHandler]
        public async Task IgnoreAsync(IgnoreBlogReportCommand command)
        {
            await _blogReportRepository.HandleAsync(command.Model.Id);

            await _unitOfWork.CommitAsync();
        }

        [EventHandler]
        public async Task AgreeAsync(AgreeBlogReportCommand command)
        {
            await _blogReportRepository.HandleByArticleId(command.Model.ArticleId);

            await _blogArticleRepository.WithdrawAsync(command.Model.ArticleId, "经举报核实后下架");

            await _unitOfWork.CommitAsync();
        }
    }
}
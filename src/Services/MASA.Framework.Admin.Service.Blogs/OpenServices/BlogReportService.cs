using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Querys;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogReportService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogReportService(IServiceCollection services) : base(services, "/api/blog-report")
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            MapPost(GetListAsync);
            MapPost(CreateAsync);
            MapPost(IgnoreAsync);
            MapPost(AgreeAsync);
        }

        public async Task<IResult> GetListAsync(GetBlogReportOptions options)
        {
            var blogQuery = new BlogReportListQuery
            {
                Options = options
            };
            await _eventBus.PublishAsync(blogQuery);

            return Results.Ok(blogQuery.Result);
        }

        public async Task CreateAsync(CreateBlogReportModel request)
        {
            try
            {
                await _eventBus.PublishAsync(new CreateBlogReportCommand(request));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task IgnoreAsync(IgnoreBlogReportModel request)
        {
            await _eventBus.PublishAsync(new IgnoreBlogReportCommand(request));
        }

        public async Task AgreeAsync(AgreeBlogReportModel request)
        {
            await _eventBus.PublishAsync(new AgreeBlogReportCommand(request));
        }
    }
}
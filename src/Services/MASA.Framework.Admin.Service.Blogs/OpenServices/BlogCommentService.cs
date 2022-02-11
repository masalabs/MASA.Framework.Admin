using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Querys;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogCommentService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogCommentService(IServiceCollection services) : base(services, "/api/blogComment")
        {
            _eventBus = GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            MapPost(CreateAsync);
            MapPost(RemoveAsync);
            MapPost(GetListAsync);
        }

        public async Task CreateAsync(AddCommentModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogCommentCommand(request));
        }

        public async Task RemoveAsync(RemoveCommentModel request)
        {
            await _eventBus.PublishAsync(new RemoveBlogCommentCommand(request));
        }

        public async Task<IResult> GetListAsync(GetBlogCommentInfoOptions options)
        {
            var blogQuery = new BlogCommentListQuery
            {
                Options = options
            };
            await _eventBus.PublishAsync(blogQuery);

            return Results.Ok(blogQuery.Result);
        }
    }
}

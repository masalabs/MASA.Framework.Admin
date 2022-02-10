using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogArticleService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogArticleService(IServiceCollection services) : base(services, "api/articles")
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            MapPost(GetListAsync);
            MapPost(CreateAsync);
            MapPut(UpdateAsync);
            MapDelete(RemoveAsync);
            MapGet(GetAsync);
        }

        public async Task<IResult> GetListAsync(GetBlogArticleOptions options)
        {
            var blogQuery = new BlogArticleQuery
            {
                Options = options
            };
            await _eventBus.PublishAsync(blogQuery);

            return Results.Ok(blogQuery.Result);
        }

        public async Task<IResult> GetAsync(Guid id)
        {
            var blogDetailsQuery = new BlogArticleDetailsQuery
            {
                Id = id
            };
            await _eventBus.PublishAsync(blogDetailsQuery);

            return Results.Ok(blogDetailsQuery.Result);
        }

        public async Task CreateAsync(CreateBlogInfoModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogInfoCommand(request));
        }

        public async Task UpdateAsync(UpdateBlogInfoModel request)
        {
            await _eventBus.PublishAsync(new UpdateBlogInfoCommand(request));
        }

        public async Task RemoveAsync([FromBody] Guid[] ids)
        {
            await _eventBus.PublishAsync(new RemoveBlogInfoCommand(ids));
        }
    }
}

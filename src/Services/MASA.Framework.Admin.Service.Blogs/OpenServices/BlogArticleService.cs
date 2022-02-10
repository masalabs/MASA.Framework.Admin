using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Contracts.Blogs.BlogInfo.Options;
using MASA.Framework.Admin.Contracts.Blogs.Model.BlogInfo.Model;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogArticleService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogArticleService(IServiceCollection services) : base(services, "api/blogs")
        {
            MapPost(GetListAsync, "/api/blogs/articles");
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

        public async Task CreateAsync(CreateBlogInfoModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogInfoCommand(request));
        }

        public async Task UpdateAsync(UpdateBlogInfoModel request)
        {
            await _eventBus.PublishAsync(new UpdateBlogInfoCommand(request));
        }
    }
}

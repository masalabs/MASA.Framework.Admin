using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Model;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogArticleService : ServiceBase
    {
        public BlogArticleService(IServiceCollection services) : base(services)
        {

        }

        public async Task<IResult> GetListAsync(GetBlogArticleOptions options, [FromServices] IEventBus eventBus)
        {

            var blogQuery = new BlogArticleQuery
            {
                Options = options
            };
            await eventBus.PublishAsync(blogQuery);
            if (blogQuery.Result is null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(BlogInfoModel.FromOrder(blogQuery.Result));
            }
        }
    }
}

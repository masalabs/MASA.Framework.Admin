using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos;
using MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Querys;
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
            MapPost(BlogArticleHomeAsync);
            MapPost(BlogArticleByUserAsync);
            MapPost(AddVisitsAsync);
            MapPost(AddBlogApprovedRecordAsync);
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

        public async Task<IResult> GetAsync(Guid id, Guid? userId)
        {
            var blogDetailsQuery = new BlogArticleDetailsQuery
            {
                Id = id,
                UserId = userId.HasValue ? userId.Value : Guid.Empty
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

        public async Task<IResult> BlogArticleHomeAsync(GetBlogArticleHomeOptions options)
        {
            var query = new BlogArticleHomeQuery
            {
                Options = options
            };
            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }

        public async Task<IResult> BlogArticleByUserAsync(GetBlogArticleUserOptions options)
        {
            var query = new BlogArticleUserQuery
            {
                Options = options
            };
            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }

        public async Task AddVisitsAsync(AddBlogVisitModel request)
        {
            await _eventBus.PublishAsync(new AddBlogVisitCommand(request));
        }

        /// <summary>
        /// 点赞、取消点赞记录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task AddBlogApprovedRecordAsync(BlogApprovedRecordModel request)
        {
            await _eventBus.PublishAsync(new AddBlogApprovedRecordCommand(request));
        }
    }
}

using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogTypeService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogTypeService(IServiceCollection services) : base(services)
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            App.MapPost("/api/blogtype/create", CreateAsync);
            App.MapPost("/api/blogtype/update", UpdateAsync);
            App.MapPost("/api/blogtype/remove", RemoveAsync);
            App.MapPost("/api/blogtype/paging", GetListAsync);
            App.MapGet("/api/blogtype/all", GetCondensedListAsync);
        }

        public async Task CreateAsync(CreateBlogTypeModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogTypeCommand(request));
        }

        public async Task UpdateAsync(UpdateBlogTypeModel request)
        {
            await _eventBus.PublishAsync(new UpdateBlogTypeCommand(request));
        }

        public async Task RemoveAsync(Guid[] ids)
        {
            await _eventBus.PublishAsync(new RemoveBolgTypeCommand(ids));
        }

        public async Task<IResult> GetListAsync(GetBlogTypePagingOption option)
        {
            var query = new GetBlogTypePagingQuery(option);

            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }

        public async Task<IResult> GetCondensedListAsync()
        {
            var query = new GetBlogTypeCondensedQuery();

            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }
    }
}

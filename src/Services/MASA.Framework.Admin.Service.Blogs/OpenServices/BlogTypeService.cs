using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;

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
    }
}

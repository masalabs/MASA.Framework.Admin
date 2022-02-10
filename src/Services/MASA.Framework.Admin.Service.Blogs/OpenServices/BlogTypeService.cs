using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;

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
        }


        public async Task CreateAsync(CreateBlogTypeModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogTypeCommand(request));
        }

        public async Task UpdateAsync(UpdateBlogTypeRequestModel request)
        {
            await _eventBus.PublishAsync(new UpdateBlogTypeCommand(request));
        }

        public async Task RemoveAsync(Guid[] Ids)
        {
            await _eventBus.PublishAsync(new RemoveBolgTypeCommand(Ids));
        }

    }
}

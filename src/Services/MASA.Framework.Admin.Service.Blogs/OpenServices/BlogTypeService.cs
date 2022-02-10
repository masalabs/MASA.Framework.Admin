using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogTypeService : ServiceBase
    {
        private readonly IEventBus? _eventBus;

        public BlogTypeService(IServiceCollection services) : base(services)
        {
            _eventBus = this.GetService<IEventBus>();

            App.MapPost("/api/blogtype/create", CreateAsync);
            App.MapPost("/api/blogtype/update", UpdateAsync);
            App.MapPost("/api/blogtype/remove", RemoveAsync);
        }


        public async Task CreateAsync(CreateBlogTypeCommand command)
        {

        }

        public async Task UpdateAsync(UpdateBlogTypeCommand command)
        {

        }


        public async Task RemoveAsync(RemoveBolgTypeCommand command)
        {

        }

    }
}

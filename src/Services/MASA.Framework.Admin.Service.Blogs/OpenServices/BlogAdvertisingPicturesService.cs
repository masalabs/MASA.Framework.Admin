using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Querys;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;
using MASA.Framework.Admin.Service.Blogs.Model.BlogAdvertisingPictures.Model;
using MASA.Framework.Admin.Service.Blogs.Model.BlogAdvertisingPictures.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogAdvertisingPicturesService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public BlogAdvertisingPicturesService(IServiceCollection services) : base(services)
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            App.MapPost("/api/adPictures/create", CreateAsync);
            App.MapPost("/api/adPictures/update", UpdateAsync);
            App.MapPost("/api/adPictures/remove", RemoveAsync);
            App.MapPost("/api/adPictures/paging", GetListAsync);
        }

        public async Task CreateAsync(CreateBlogAdvertisingPicturesModel request)
        {
            await _eventBus.PublishAsync(new CreateBlogAdvertisingPicturesCommand(request));
        }

        public async Task UpdateAsync(UpdateBlogAdvertisingPicturesModel request)
        {
            await _eventBus.PublishAsync(new UpdateBlogAdvertisingPicturesCommand(request));
        }

        public async Task RemoveAsync(Guid[] ids)
        {
            await _eventBus.PublishAsync(new RemoveBlogAdvertisingPicturesCommand(ids));
        }

        public async Task<IResult> GetListAsync(GetBlogAdvertisingPicturesOption option)
        {
            var query = new GetBlogAdvertisingPicturesQuery(option);

            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }
    }
}

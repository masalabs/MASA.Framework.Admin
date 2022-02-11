using MASA.BuildingBlocks.Dispatcher.Events;
using MASA.Contrib.Service.MinimalAPIs;

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
            App.MapPost("/api/adPictures/updateByStatus", UpdateByStatusAsync);
            App.MapPost("/api/adPictures/getBlogListAsync", GetBlogFrontListAsync);
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

        public async Task UpdateByStatusAsync(UpdateStatusBlogAdvertisingPicturesModel request)
        {
            await _eventBus.PublishAsync(new UpdateStatusBlogAdvertisingPicturesCommand(request));
        }

        public async Task<IResult> GetBlogFrontListAsync(GetBlogAdvertisingPicturesFrontOption option)
        {
            var query = new GetBlogAdvertisingPicturesFrontQuery(option);

            await _eventBus.PublishAsync(query);

            return Results.Ok(query.Result);
        }
    }
}

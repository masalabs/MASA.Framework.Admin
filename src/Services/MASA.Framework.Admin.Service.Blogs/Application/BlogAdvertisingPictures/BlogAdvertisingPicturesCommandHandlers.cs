using MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Commands;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Contrib.Dispatcher.Events;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures
{
    public class BlogAdvertisingPicturesCommandHandlers
    {
        private readonly IBlogAdvertisingPicturesRepository _advertisingPicturesRepository;

        public BlogAdvertisingPicturesCommandHandlers(IBlogAdvertisingPicturesRepository advertisingPicturesRepository)
        {
            this._advertisingPicturesRepository = advertisingPicturesRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.CreateAsync(new()
            {
                Title = command.Request.Title,
                Pic = command.Request.Pic,
                Sort = command.Request.Sort,
                Type = command.Request.Type,
            });
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.UpdateAsync(new()
            {
                Id = command.Id,
                Title = command.Request.Title,
                Pic = command.Request.Pic,
                Sort = command.Request.Sort,
                Type = command.Request.Type,
            });
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.RemoveAsync(command.Ids);
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateStatusBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.UpdateByStatusAsync(command.Request.Id,command.Request.Status);
        }
    }
}

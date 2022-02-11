using MASA.BuildingBlocks.Data.UoW;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures
{
    public class BlogAdvertisingPicturesCommandHandlers
    {
        private readonly IBlogAdvertisingPicturesRepository _advertisingPicturesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogAdvertisingPicturesCommandHandlers(IUnitOfWork unitOfWork,IBlogAdvertisingPicturesRepository advertisingPicturesRepository)
        {
            this._advertisingPicturesRepository = advertisingPicturesRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.CommitAsync(); 
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.UpdateAsync(new()
            {
                Id = command.Request.Id,
                Title = command.Request.Title,
                Pic = command.Request.Pic,
                Sort = command.Request.Sort,
                Type = command.Request.Type,
                Status = command.Request.Status,
            });

           await _unitOfWork.CommitAsync();
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.RemoveAsync(command.Ids);

            await _unitOfWork.CommitAsync();
        }

        [EventHandler]
        public async Task UpdateByStatusAsync(UpdateStatusBlogAdvertisingPicturesCommand command)
        {
            await _advertisingPicturesRepository.UpdateByStatusAsync(command.Request.Id,command.Request.Status);

            await _unitOfWork.CommitAsync();
        }
    }
}

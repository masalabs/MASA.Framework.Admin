using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Blogs
{
    public class AdvertisingPicturesService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public AdvertisingPicturesService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public Task<List<BlogAdvertisingPicturesListViewModel>> GetList(GetBlogAdvertisingPicturesFrontOption options)
        {
            return _callerProviderProvider.PostAsync<GetBlogAdvertisingPicturesFrontOption, List<BlogAdvertisingPicturesListViewModel>>(
                "/api/adPictures/getList", options);
        }

        public async Task CreateAsync(CreateBlogAdvertisingPicturesModel request)
        {
            await _callerProviderProvider.PostAsync<CreateBlogAdvertisingPicturesModel>(
                "/api/adPictures/create", request);
        }

        public async Task UpdateAsync(UpdateBlogAdvertisingPicturesModel request)
        {
            await _callerProviderProvider.PostAsync<UpdateBlogAdvertisingPicturesModel>(
                "/api/adPictures/update", request);
        }

        public async Task UpdateByStatusAsync(UpdateStatusBlogAdvertisingPicturesModel request)
        {
            await _callerProviderProvider.PostAsync<UpdateStatusBlogAdvertisingPicturesModel>(
                "/api/adPictures/updateByStatus", request);
        }

        public async Task RemoveAsync(Guid[] ids)
        {
            await _callerProviderProvider.PostAsync<Guid[]>(
                "/api/adPictures/remove", ids);
        }

        public async Task<PagingResult<BlogAdvertisingPicturesListViewModel>> PagingAsync(GetBlogAdvertisingPicturesOption request)
        {
            return await _callerProviderProvider.PostAsync<GetBlogAdvertisingPicturesOption, PagingResult<BlogAdvertisingPicturesListViewModel>>(
                "/api/adPictures/paging", request);
        }
    }
}

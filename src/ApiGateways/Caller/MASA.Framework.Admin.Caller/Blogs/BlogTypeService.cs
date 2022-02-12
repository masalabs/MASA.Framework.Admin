using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Blogs
{
    public class BlogTypeService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public BlogTypeService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task CreateAsync(CreateBlogTypeModel request)
        {
            await _callerProviderProvider.PostAsync<CreateBlogTypeModel>(
                 "/api/blogtype/create", request);
        }

        public async Task UpdateAsync(UpdateBlogTypeModel request)
        {
            await _callerProviderProvider.PostAsync<UpdateBlogTypeModel>(
                "/api/blogtype/update", request);
        }

        public async Task RemoveAsync(Guid[] ids)
        {
            await _callerProviderProvider.PostAsync<Guid[]>(
                "/api/blogtype/remove", ids);
        }

        public async Task<PagingResult<BlogTypePagingViewModel>> PagingAsync(GetBlogTypePagingOption request)
        {
            return await _callerProviderProvider.PostAsync<GetBlogTypePagingOption, PagingResult<BlogTypePagingViewModel>>(
                  "/api/blogtype/paging", request);
        }

        public Task<List<BlogTypeCondensedViewModel>> GetAllAsync()
        {
            return _callerProviderProvider.GetAsync<List<BlogTypeCondensedViewModel>>("/api/blogtype/all");
        }
    }
}

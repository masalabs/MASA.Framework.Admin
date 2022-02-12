using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Model;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;
using MASA.Utils.Caller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Management
{
    public class DicService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public DicService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task<PagingResult<DicViewModel>> PagingAsync(DicPagingOptions options)
        {
            return await _callerProviderProvider.PostAsync<DicPagingOptions, PagingResult<DicViewModel>>(
                  "/api/dic/getPage", options);
        }

        public async Task<DicViewModel> GetAsync(Guid id)
        {
            return await _callerProviderProvider.GetAsync<DicViewModel>($"/api/dic/get/{id}");
        }

        public async Task<Guid> CreateAsync(AddDicModel model)
        {
            return await _callerProviderProvider.PostAsync<AddDicModel, Guid>(
                 "/api/dic/create", model);
        }

        public async Task<Guid> UpdateAsync(UpdateDicModel model)
        {
            return await _callerProviderProvider.PostAsync<UpdateDicModel, Guid>(
                 "/api/dic/update", model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _callerProviderProvider.PostAsync($"/api/dic/delete/{id}", null);
        }

        public async Task DeleteAsync(List<Guid> ids)
        {
            await _callerProviderProvider.PostAsync($"/api/dic/deleteAll", ids);
        }
    }
}

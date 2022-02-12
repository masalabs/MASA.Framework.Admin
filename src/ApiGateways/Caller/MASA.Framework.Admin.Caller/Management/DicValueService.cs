using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Model;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Model;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;
using MASA.Utils.Caller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Management
{
    public class DicValueService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public DicValueService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task<PagingResult<DicValueViewModel>> PagingAsync(DicValuePagingOptions options)
        {
            return await _callerProviderProvider.PostAsync<DicValuePagingOptions, PagingResult<DicValueViewModel>>(
                  "/api/dicValue/getPage", options);
        }

        public async Task<DicValueViewModel> GetAsync(Guid id)
        {
            return await _callerProviderProvider.GetAsync<DicValueViewModel>($"/api/dicValue/get/{id}");
        }

        public async Task<Guid> CreateAsync(AddDicValueModel model)
        {
            return await _callerProviderProvider.PostAsync<AddDicValueModel, Guid>(
                 "/api/dicValue/create", model);
        }

        public async Task<Guid> UpdateAsync(UpdateDicValueModel model)
        {
            return await _callerProviderProvider.PostAsync<UpdateDicValueModel, Guid>(
                 "/api/dicValue/update", model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _callerProviderProvider.PostAsync($"/api/dicValue/delete/{id}", null);
        }

        public async Task DeleteAsync(List<Guid> ids)
        {
            await _callerProviderProvider.PostAsync($"/api/dicValue/deleteAll", ids);
        }
    }
}

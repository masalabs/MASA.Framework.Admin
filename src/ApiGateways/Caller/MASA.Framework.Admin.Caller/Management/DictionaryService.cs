using MASA.Framework.Admin.Contracts.Dictionary;
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
    public class DictionaryService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public DictionaryService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task<PagingResult<DicViewModel>> PagingAsync(DicPagingOptions options)
        {
            return await _callerProviderProvider.PostAsync<DicPagingOptions, PagingResult<DicViewModel>>(
                  "/api/dic/getPage", options);
        }
    }
}

using MASA.Framework.Admin.Caller.Model;
using MASA.Utils.Caller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Management
{
    public class JobService
    {
        private readonly ICallerProvider _callerProviderProvider;

        public JobService(ICallerProvider callerProvider)
        {
            _callerProviderProvider = callerProvider;
        }

        public async Task<PagingResult<JobViewModel>> PagingAsync(JobPagingOptions options)
        {
            return await _callerProviderProvider.PostAsync<JobPagingOptions, PagingResult<JobViewModel>>(
                  "/api/job/page", options);
        }

        public async Task<Guid> CreateAsync(AddJobRequest model)
        {
            return await _callerProviderProvider.PostAsync<AddJobRequest, Guid>(
                 "/api/job/create", model);
        }

        public async Task<Guid> UpdateAsync(UpdateJobRequest model)
        {
            return await _callerProviderProvider.PostAsync<UpdateJobRequest, Guid>(
                 "/api/job/update", model);
        }

        public async Task<PagingResult<JobLogViewModel>> LogPagingAsync(JobLogPagingOptions options)
        {
            return await _callerProviderProvider.PostAsync<JobLogPagingOptions, PagingResult<JobLogViewModel>>(
                  "/api/job/logpage", options);
        }
    }
}

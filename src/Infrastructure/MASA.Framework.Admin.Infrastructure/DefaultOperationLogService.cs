using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructure
{
    public class DefaultOperationLogService : IOperationLogService
    {
        private readonly HttpClient _httpClient;

        public DefaultOperationLogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MASA.Framework.Admin.Api");
        }

        public async Task LogAsync(string description, OperationLogType operationLogType)
        {
            var viewModel = new OperationLogViewModel
            {
                Description = description,
                OperationLogType = operationLogType
            };
            await _httpClient.PostAsJsonAsync("/api/operationLog", viewModel);
        }
    }
}

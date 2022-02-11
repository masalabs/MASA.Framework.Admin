using MASA.Framework.Admin.Models;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Web.Pages.Dashboard
{
    public partial class Index
    {
        private HttpClient _httpClient;
        private IEnumerable<OperationLogDto> _operationLogs;
        private int _count;

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MASA.Framework.Admin.Api");
            await UpdateDataAsync();
        }

        private async ValueTask<ItemsProviderResult<OperationLogDto>> LoadOperationLogs(ItemsProviderRequest request)
        {
            var offset = request.StartIndex;
            var limit = Math.Min(request.Count, _count - request.StartIndex);

            await UpdateDataAsync(offset, limit);
            return new ItemsProviderResult<OperationLogDto>(_operationLogs, _count);
        }

        private async Task UpdateDataAsync(int offset = 1, int limit = 10)
        {
            var query = $"?offset={offset}&limit={limit}";
            var pageResult = await _httpClient.GetFromJsonAsync<PageResult<OperationLogDto>>("/operationLog" + query);

            _operationLogs = pageResult.Data;
            _count = pageResult.Count;
        }
    }
}

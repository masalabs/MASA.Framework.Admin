namespace MASA.Framework.Admin.Web.Services
{
    public class DefaultOperationLogService : IOperationLogService
    {
        private readonly HttpClient _httpClient;

        public DefaultOperationLogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Logging");
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

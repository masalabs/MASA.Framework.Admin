using Masa.Framework.Sdks.Authentication.Request.LogStatistics;
using Masa.Framework.Sdks.Authentication.Response.LogStatistics;

namespace Masa.Framework.Sdks.Authentication.Callers
{
    public class LogStatisticsCaller : CallerBase
    {
        protected override string BaseAddress { get; set; }


        public LogStatisticsCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            Name = nameof(LogStatisticsCaller);
            BaseAddress = configuration["ApiGateways:LogStatisticsCaller"];
        }

        protected override IHttpClientBuilder UseHttpClient()
        {
            return base.UseHttpClient().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }

        public async Task<ApiResultResponse<List<StatisticsQueryResponse>>> GetDayStatisticsAsync(DateTime start, DateTime end)
        {
            var paramters = new Dictionary<string, string>
            {
                ["StartTime"] = start.ToString(),
                ["EndTime"] = end.ToString()
            };

            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.GetAsync<List<StatisticsQueryResponse>>(Routing.DayStatistics, paramters);
                return response!;
            });
        }

        public async Task<ApiResultResponse<List<StatisticsQueryResponse>>> GetHourStatisticsAsync(DateTime start, DateTime end)
        {
            var paramters = new Dictionary<string, string>
            {
                ["StartTime"] = start.ToString(),
                ["EndTime"] = end.ToString()
            };
            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.GetAsync<List<StatisticsQueryResponse>>(Routing.HourStatistics, paramters);
                return response!;
            });
        }

        public async Task<ApiResultResponseBase> CreateLogAsync(OperationLogCreateRequest operationLogCreateRequest)
        {
            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.PostAsync(Routing.OperateLog, operationLogCreateRequest);
                return response!;
            });
        }

        public async Task<ApiResultResponse<PaginatedItemResponse<OperationLogItemResponse>>> GetLogListAsync(int pageIndex = 1, int pageSize = 20)
        {
            var queryArguments = new Dictionary<string, string?>()
            {
                { "pageIndex", pageIndex.ToString() },
                { "pageSize", pageSize.ToString() },
            };

            return await ResultAsync(async () =>
            {
                var url = QueryHelpers.AddQueryString(Routing.LogList, queryArguments);
                var response = await CallerProvider.GetAsync<PaginatedItemResponse<OperationLogItemResponse>>(url);
                return response!;
            });
        }
    }
}

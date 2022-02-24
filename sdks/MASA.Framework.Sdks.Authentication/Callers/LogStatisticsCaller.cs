using MASA.Framework.Sdks.Authentication.Request.LogStatistics;
using MASA.Framework.Sdks.Authentication.Response.LogStatistics;

namespace MASA.Framework.Sdks.Authentication.Callers
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

        public async Task<ApiResultResponse<List<StatisticsQueryResponse>>> GetDayStatisticsAsync()
        {

            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.GetAsync<List<StatisticsQueryResponse>>("");
                return response!;
            });
        }

        public async Task<ApiResultResponse<List<StatisticsQueryResponse>>> GetHourStatisticsAsync()
        {

            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.GetAsync<List<StatisticsQueryResponse>>("");
                return response!;
            });
        }

        public async Task<ApiResultResponseBase> CreateLogAsync(OperationLogCreateRequest operationLogCreateRequest)
        {
            return await ResultAsync(async () =>
            {
                var response = await CallerProvider.PostAsync("", operationLogCreateRequest);
                return response!;
            });
        }
    }
}

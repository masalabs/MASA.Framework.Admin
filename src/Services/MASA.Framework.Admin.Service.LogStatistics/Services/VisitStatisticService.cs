using MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queres;
using MASA.Framework.Sdks.Authentication.Internal;
using MASA.Framework.Sdks.Authentication.Internal.Enum;
using MASA.Framework.Sdks.Authentication.Request.LogStatistics;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.LogStatistics.Services
{
    public class VisitStatisticService : ServiceBase
    {
        public VisitStatisticService(IServiceCollection services) : base(services)
        {
            App.MapGet(Routing.DayStatistics, DayStatisticsAsync);
            App.MapGet(Routing.HourStatistics, HourStatisticsAsync);
        }

        public async Task DayStatisticsAsync(
                    [FromServices] IEventBus eventBus,
                    [FromBody] StatisticsQueryRequest statisticsQueryRequest)
        {
            var statisticsQuery = new StatisticsQuery(statisticsQueryRequest, VisitStatisticType.Day);
            await eventBus.PublishAsync(statisticsQuery);
        }

        public async Task HourStatisticsAsync(
                    [FromServices] IEventBus eventBus,
                    [FromBody] StatisticsQueryRequest statisticsQueryRequest)
        {
            var statisticsQuery = new StatisticsQuery(statisticsQueryRequest, VisitStatisticType.Hour);
            await eventBus.PublishAsync(statisticsQuery);
        }

    }
}

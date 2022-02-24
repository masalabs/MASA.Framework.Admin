using MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queres;
using MASA.Framework.Sdks.Authentication.Internal;
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

        public async Task<List<StatisticsQueryResponse>> DayStatisticsAsync(
                    [FromServices] IEventBus eventBus,
                    [FromQuery] DateTime startTime,
                    [FromQuery] DateTime endTime)
        {
            var statisticsQuery = new StatisticsQuery(startTime, endTime, VisitStatisticType.Day);
            await eventBus.PublishAsync(statisticsQuery);
            return statisticsQuery.Result;
        }

        public async Task<List<StatisticsQueryResponse>> HourStatisticsAsync(
                    [FromServices] IEventBus eventBus,
                    [FromQuery] DateTime startTime,
                    [FromQuery] DateTime endTime)
        {
            var statisticsQuery = new StatisticsQuery(startTime, endTime, VisitStatisticType.Hour);
            await eventBus.PublishAsync(statisticsQuery);
            return statisticsQuery.Result;
        }

    }
}

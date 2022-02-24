using MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queres;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Repositories;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.Statistics
{
    public class QueryHandler
    {
        readonly IVisitStatisticsRecordRepository _visitStatisticsRecordRepository;

        public QueryHandler(IVisitStatisticsRecordRepository visitStatisticsRecordRepository)
        {
            _visitStatisticsRecordRepository = visitStatisticsRecordRepository;
        }

        [EventHandler]
        public async Task GetListAsync(StatisticsQuery statisticsQuery)
        {

        }
    }
}

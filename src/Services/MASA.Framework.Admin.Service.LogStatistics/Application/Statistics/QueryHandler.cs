using MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queres;

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
            var records = await _visitStatisticsRecordRepository.GetListAsync(record => record.Type == statisticsQuery.StatisticType);
            statisticsQuery.Result = records.Select(record => new StatisticsQueryResponse
            {
                Id = record.Id,
                PV = record.PV,
                UV = record.UV,
                IPCount = record.IPCount,
                DateTime = record.DateTime
            }).ToList();
        }
    }
}

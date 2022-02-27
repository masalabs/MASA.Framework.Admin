namespace MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queries
{
    public record StatisticsQuery(DateTime StartTime, DateTime EndTime, VisitStatisticType StatisticType) : Query<List<StatisticsQueryResponse>>
    {
        public override List<StatisticsQueryResponse> Result { get; set; } = new List<StatisticsQueryResponse>();
    }
}

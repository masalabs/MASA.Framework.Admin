using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Sdks.Authentication.Internal.Enum;
using MASA.Framework.Sdks.Authentication.Request.LogStatistics;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Queres
{
    public record StatisticsQuery(StatisticsQueryRequest StatisticsQueryRequest, VisitStatisticType StatisticType) : Query<List<StatisticsQuery>>
    {
        public override List<StatisticsQuery> Result { get; set; } = new List<StatisticsQuery>();
    }
}

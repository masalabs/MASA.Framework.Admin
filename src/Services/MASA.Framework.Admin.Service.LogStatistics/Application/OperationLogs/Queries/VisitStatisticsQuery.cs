namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queries;

public record VisitStatisticsQuery(DateTime StartTime, DateTime EndTime) : Query<OperationStatisticsModel>
{
    public override OperationStatisticsModel Result { get; set; } = new();
}


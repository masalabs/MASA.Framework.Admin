namespace Masa.Framework.Admin.Service.LogStatistics.Application.OperationLogs;

public class QueryHandler
{
    readonly IOperationLogRepository _operationLogRepository;

    public QueryHandler(IOperationLogRepository operationLogRepository)
    {
        _operationLogRepository = operationLogRepository;
    }

    [EventHandler]
    public async Task GetListAsync(ListQuery listQuery)
    {
        var logs = await _operationLogRepository.GetPaginatedListAsync((u) => string.IsNullOrEmpty(listQuery.Description) || u.Description.Contains(listQuery.Description)
                    , new PaginatedOptions
                    {
                        Page = listQuery.PageIndex,
                        PageSize = listQuery.PageSize,
                    });

        listQuery.Result = new PaginatedItemResponse<OperationLogItemResponse>(
            listQuery.PageIndex,
            listQuery.PageSize,
            logs.Total,
            logs.TotalPages,
            logs.Result.Select(log => new OperationLogItemResponse()
            {
                Id = log.Id,
                Description = log.Description,
                CreateTime = log.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                UserName = "admin"
            }));
    }

    [EventHandler]
    public async Task VisitStatisticsAsync(VisitStatisticsQuery visitStatisticsQuery)
    {
        var startTime = visitStatisticsQuery.StartTime;
        var endTime = visitStatisticsQuery.EndTime;
        var visitLogs = await _operationLogRepository.GetListAsync(log => log.Type == OperationLogType.VisitPage && log.CreationTime >= startTime && log.CreationTime <= endTime);
        visitStatisticsQuery.Result = new OperationStatisticsModel
        {
            PV = visitLogs.Count(),
            UV = visitLogs.GroupBy(log => log.UserId).Count(),
            IpCount = visitLogs.GroupBy(log => log.ClientIP).Count(),
        };
    }
}


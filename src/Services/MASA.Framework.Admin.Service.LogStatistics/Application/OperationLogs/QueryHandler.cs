using MASA.BuildingBlocks.DDD.Domain.Repositories;
using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Infrastructure.Configurations.Response;
using MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queres;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Repositories;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs
{
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
    }
}

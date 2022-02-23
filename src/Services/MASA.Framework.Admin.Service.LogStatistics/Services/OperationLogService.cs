using MASA.Framework.Admin.Contracts.Base.Request;
using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Infrastructure.Configurations.Response;
using MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands;
using MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queres;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.LogStatistics.Services
{
    public class OperationLogService : ServiceBase
    {
        public OperationLogService(IServiceCollection services) : base(services)
        {
            App.MapPost("", CreateLogAsync);
            App.MapGet("", GetItemsAsync);
        }

        public async Task<PaginatedItemResponse<OperationLogItemResponse>> GetItemsAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string description = "")
        {
            var listQuery = new ListQuery(pageIndex, pageSize, description);
            await eventBus.PublishAsync(listQuery);
            return listQuery.Result;
        }

        public async Task CreateLogAsync(
            [FromServices] IEventBus eventBus,
            [FromHeader(Name = "creator-id")] Guid creator,
            [FromBody] OperationLogCreateRequest operationLogCreateRequest)
        {
            var createCommand = new CreateCommand(operationLogCreateRequest);
            await eventBus.PublishAsync(createCommand);
        }
    }
}

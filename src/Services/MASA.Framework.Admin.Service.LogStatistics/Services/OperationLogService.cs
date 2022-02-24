using MASA.Framework.Admin.Infrastructure.Configurations.Response;
using MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands;
using MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queres;
using MASA.Framework.Sdks.Authentication.Internal;
using MASA.Framework.Sdks.Authentication.Request.LogStatistics;
using MASA.Framework.Sdks.Authentication.Response.LogStatistics;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.LogStatistics.Services
{
    public class OperationLogService : ServiceBase
    {
        public OperationLogService(IServiceCollection services) : base(services)
        {
            App.MapPost(Routing.OperateLog, CreateLogAsync);
            App.MapGet(Routing.LogList, GetItemsAsync);
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

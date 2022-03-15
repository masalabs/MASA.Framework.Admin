using IdListQuery = Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries.IdListQuery;

namespace Masa.Framework.Admin.Service.Authentication.Services
{
    public class PermissionService : ServiceBase
    {
        public PermissionService(IServiceCollection services) : base(services)
        {
            App.MapGet(Routing.PermissionList, GetItemsAsync);
            App.MapGet(Routing.OperatePermission, GetDetailAsync);
            App.MapGet(Routing.PermissionListByIds, GetItemsByIdAsync);
            App.MapPost(Routing.OperatePermission, AddAsync);
            App.MapPut(Routing.OperatePermission, EditAsync);
        }

        private async Task<PaginatedItemResponse<PermissionItemResponse>> GetItemsAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] int pageIndex = QueryConfig.DEFAULT_PAGE_INDEX,
            [FromQuery] int pageSize = QueryConfig.DEFAULT_PAGE_SIZE,
            [FromQuery] string name = "",
            [FromQuery] bool enabled = true)
        {
            var query = new PermissionListQuery(pageIndex, pageSize, name, enabled);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        private async Task<PermissionDetailResponse> GetDetailAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] Guid id)
        {
            var query = new PermissionDetailQuery(id);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        private async Task<List<PermissionItemResponse>> GetItemsByIdAsync(
       [FromServices] IEventBus eventBus,
       [FromQuery] string permissionIds)
        {
            var query = new IdListQuery(JsonSerializer.Deserialize<List<Guid>>(permissionIds) ?? new());
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        public async Task AddAsync(
            [FromServices] IEventBus eventBus,
            [FromBody] AddPermissionCommand command)
        {
            await eventBus.PublishAsync(command);
        }

        public async Task EditAsync(
            [FromServices] IEventBus eventBus,
            [FromBody] EditPermissionCommand command)
        {
            await eventBus.PublishAsync(command);
        }
    }
}

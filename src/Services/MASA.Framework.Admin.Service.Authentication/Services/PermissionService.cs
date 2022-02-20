namespace MASA.Framework.Admin.Service.Authentication.Services
{
    public class PermissionService : ServiceBase
    {
        public PermissionService(IServiceCollection services) : base(services)
        {
            App.MapGet(Routing.PermissionList, GetItemsAsync);
        }

        private async Task<PaginatedItemResponse<PermissionItemResponse>> GetItemsAsync(
            [FromServices] IEventBus eventBus,
            [FromQuery] int pageIndex = QueryConfig.DEFAULT_PAGE_INDEX,
            [FromQuery] int pageSize = QueryConfig.DEFAULT_PAGE_SIZE,
            [FromQuery] string name = "",
            [FromQuery] int state = -1)
        {
            var query = new PermissionListQuery(pageIndex, pageSize, name, state);
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
    }
}

namespace MASA.Framework.Admin.Service.Authentication.Services;

public class RoleService : ServiceBase
{
    public RoleService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.RoleList, GetItemsAsync);
        App.MapGet(Routing.RoleDetail, GetAsync);
        App.MapGet(Routing.RoleSelect, GetSelectListAsync);
        App.MapGet(Routing.RoleListByIds, GetItemsByIdAsync);
        App.MapPost(Routing.OperateRole, CreateAsync);
        App.MapPut(Routing.OperateRole, EditAsync);
        App.MapDelete(Routing.OperateRole, DeleteAsync);
    }

    private async Task<PaginatedItemResponse<RoleItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = QueryConfig.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = QueryConfig.DEFAULT_PAGE_SIZE,
        [FromQuery] string name = "",
        [FromQuery] int state = -1)
    {
        var query = new RoleListQuery(pageIndex, pageSize, name, state);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<List<RoleItemResponse>> GetSelectListAsync([FromServices] IEventBus eventBus)
    {
        var query = new SelectQuery();
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<List<RoleItemResponse>> GetItemsByIdAsync(
        [FromServices] IEventBus eventBus, [FromQuery] string roleIds)
    {
        var query = new IdListQuery(JsonSerializer.Deserialize<List<Guid>>(roleIds) ?? new());
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<RoleDetailResponse> GetAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid id)
    {
        var query = new RoleDetailQuery(id);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddRoleCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    private async Task EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditRoleCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    private async Task DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] DeleteRoleCommand command)
    {
        await eventBus.PublishAsync(command);
    }
}

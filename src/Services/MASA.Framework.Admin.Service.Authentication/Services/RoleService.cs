using IdListQuery = Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries.IdListQuery;

namespace Masa.Framework.Admin.Service.Authentication.Services;

public class RoleService : ServiceBase
{
    public RoleService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.RoleList, GetItemsAsync);
        App.MapGet(Routing.RoleDetail, GetAsync);
        App.MapGet(Routing.RoleSelect, GetSelectListAsync);
        App.MapGet(Routing.AllRoleItem, GetAllRoleItemAsync);
        App.MapGet(Routing.RoleListByIds, GetItemsByIdAsync);
        App.MapGet(Routing.PermissionsByRoles, GetPermissionsByRolesAsync);
        App.MapPost(Routing.OperateRole, CreateAsync);
        App.MapPost(Routing.AddChildRoles, AddChildRolesAsync);
        App.MapPut(Routing.OperateRole, EditAsync);
        App.MapDelete(Routing.OperateRole, DeleteAsync);
        App.MapDelete(Routing.RolePermission, DeleteRolePermissionAsync);
        App.MapPost(Routing.AddRolePermission, AddRolePermissionAsync);
        App.MapDelete(Routing.DeleteRolePermission, DeleteRolePermissionAsync);
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

    private async Task<List<RoleItemsResponse>> GetAllRoleItemAsync([FromServices] IEventBus eventBus)
    {
        var query = new AllRoleItemQuery();
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<List<RoleItemResponse>> GetItemsByIdAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] string roleIds)
    {
        var query = new IdListQuery(JsonSerializer.Deserialize<List<Guid>>(roleIds) ?? new());
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<List<AuthorizeItemResponse>> GetPermissionsByRolesAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] string roleIds)
    {
        var query = new PermissionsByRolesQuery(JsonSerializer.Deserialize<List<Guid>>(roleIds) ?? new());
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

    private async Task AddChildRolesAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddChildrenRolesCommand command)
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

    public async Task DeleteRolePermissionAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] DeleteRolePermissionCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    public async Task AddRolePermissionAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddRolePermissionDomainCommand command)
    {
        await eventBus.PublishAsync(command);
    }
}

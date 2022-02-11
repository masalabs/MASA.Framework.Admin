namespace MASA.Framework.Admin.Service.Authentication.Services;

public class RoleService : CustomServiceBase
{
    public RoleService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.RoleList, GetItemsAsync);
        App.MapPost(Routing.OperateRole, CreateAsync);
        App.MapPut(Routing.OperateRole, EditAsync);
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string name = "",
        [FromQuery] int state = -1)
    {
        var query = new RoleQuery.ListQuery(pageIndex, pageSize, name, state);
        await eventBus.PublishAsync(query);
        return Success(query.Result);
    }

    public async Task<ApiResultResponseBase> CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddRoleRequest request)
    {
        await eventBus.PublishAsync(new RoleCommand.AddCommand(request));
        return Success();
    }

    public async Task<ApiResultResponseBase> EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditRoleRequest request)
    {
        await eventBus.PublishAsync(new RoleCommand.EditCommand(request));
        return Success();
    }
}

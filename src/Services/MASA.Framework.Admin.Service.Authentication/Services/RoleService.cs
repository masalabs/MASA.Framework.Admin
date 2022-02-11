namespace MASA.Framework.Admin.Service.Authentication.Services;

public class RoleService : CustomServiceBase
{
    public RoleService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.RoleList, GetItemsAsync);
        App.MapPost(Routing.OperateRole, CreateAsync);
        App.MapPut(Routing.OperateRole, EditAsync);
    }

    public ApiResultResponse<PaginatedItemResponse<RoleItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string name = "",
        [FromQuery] int state = -1)
    {
        var response = new PaginatedItemResponse<RoleItemResponse>(pageIndex, pageSize, 0, new List<RoleItemResponse>());
        return Success(response);
    }

    public ApiResultResponseBase CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddRuleCommand command)
    {
        return Success();
    }

    public ApiResultResponseBase EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditRuleCommand command)
    {
        return Success();
    }
}

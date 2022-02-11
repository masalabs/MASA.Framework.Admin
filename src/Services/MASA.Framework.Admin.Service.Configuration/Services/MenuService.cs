namespace MASA.Framework.Admin.Configuration.Services;

public class MenuService : CustomServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.MenuList, GetItemsAsync);
        App.MapPost(Routing.OperateMenu, CreateAsync);
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE,
        [FromQuery] string name = "")
    {
        var query = new MenuQuery.ListQuery(pageIndex, pageSize, name);
        await eventBus.PublishAsync(query);
        return Success(query.Result);
    }

    public async Task<ApiResultResponseBase> CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddMenuRequest request)
    {
        await eventBus.PublishAsync(new MenuCommand.AddCommand(request));
        return Success();
    }
}

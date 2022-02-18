namespace MASA.Framework.Admin.Configuration.Services;

public class MenuService : CustomServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.MenuList, GetItemsAsync);
        App.MapGet(Routing.AllMenus, GetAllAsync);
        App.MapGet(Routing.AnyChild, AnyChildAsync);
        App.MapPost(Routing.OperateMenu, CreateAsync);
        App.MapPut(Routing.OperateMenu, EditAsync);
        App.MapDelete(Routing.OperateMenu, DeleteAsync);
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

    public async Task<ApiResultResponse<List<MenuItemResponse>>> GetAllAsync(
        [FromServices] IEventBus eventBus)
    {
        var query = new MenuQuery.AllQuery();
        await eventBus.PublishAsync(query);
        return Success(query.Result);
    }

    public async Task<ApiResultResponse<bool>> AnyChildAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid menuId)
    {
        var query = new MenuQuery.AnyChildQuery(menuId);
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

    public async Task<ApiResultResponseBase> EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditMenuRequest request)
    {
        await eventBus.PublishAsync(new MenuCommand.EditCommand(request));
        return Success();
    }

    public async Task<ApiResultResponseBase> DeleteAsync(
    [FromServices] IEventBus eventBus,
    [FromBody] DeleteMenuRequest request)
    {
        await eventBus.PublishAsync(new MenuCommand.DeleteCommand(request));
        return Success();
    }
}

namespace MASA.Framework.Admin.Configuration.Services;

public class MenuService : ServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.MenuList, GetItemsAsync);
        App.MapGet(Routing.AllMenus, GetAllAsync);
        App.MapGet(Routing.AnyChild, AnyChildAsync);
        App.MapPost(Routing.OperateMenu, CreateAsync);
        App.MapPut(Routing.OperateMenu, EditAsync);
        App.MapDelete(Routing.OperateMenu, DeleteAsync);
        App.MapDelete(Routing.DeleteMenuByIds, DeleteByIdsAsync);
    }

    private async Task<PaginatedItemResponse<MenuItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = QueryConfig.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = QueryConfig.DEFAULT_PAGE_SIZE,
        [FromQuery] string name = "")
    {
        var query = new MenuQuery.ListMenuQuery(pageIndex, pageSize, name);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<List<MenuItemResponse>> GetAllAsync(
        [FromServices] IEventBus eventBus)
    {
        var query = new MenuQuery.AllMenuQuery();
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task<bool> AnyChildAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] Guid menuId)
    {
        var query = new MenuQuery.AnyMenuChildQuery(menuId);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    private async Task CreateAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] AddMenuCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    private async Task EditAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] EditMenuCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    private async Task DeleteAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] DeleteMenuCommand command)
    {
        await eventBus.PublishAsync(command);
    }

    public async Task DeleteByIdsAsync(
        [FromServices] IEventBus eventBus,
        [FromBody] DeleteMenuByIdsCommand command)
    {
        await eventBus.PublishAsync(command);
    }
}

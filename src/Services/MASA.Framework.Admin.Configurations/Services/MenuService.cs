namespace MASA.Framework.Admin.Configurations.Services;

public class MenuService : ServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet("/api/configurations/menu/items", GetItemsAsync);
    }

    private PaginatedItemsViewModel<MenuItemResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        return new PaginatedItemsViewModel<MenuItemResponse>(pageIndex, pageSize, 0, new List<MenuItemResponse>());
    }
}

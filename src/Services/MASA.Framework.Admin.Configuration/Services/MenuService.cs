namespace MASA.Framework.Admin.Configuration.Services;

public class MenuService : ServiceBase
{
    private const string PRE = "configurations";

    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(UrlRule.MENU_SERVICE, GetItemsAsync);
    }

    private PaginatedItemsViewModel<MenuItemResponse> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        return new PaginatedItemsViewModel<MenuItemResponse>(pageIndex, pageSize, 0, new List<MenuItemResponse>());
    }
}

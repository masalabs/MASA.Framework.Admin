namespace MASA.Framework.Admin.Configuration.Services;

public class MenuService : CustomServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(Contracts.Configuration.Const.UrlRule.MENU_SERVICE, GetItemsAsync);
    }

    private ApiResultResponse<PaginatedItemsViewModel<MenuItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        var response = new PaginatedItemsViewModel<MenuItemResponse>(pageIndex, pageSize, 0, new List<MenuItemResponse>());
        return Success(response);
    }
}

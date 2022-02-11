using MASA.Framework.Admin.Contracts.Configuration;
using MASA.Framework.Admin.Contracts.Configuration.Response;

namespace MASA.Framework.Admin.Service.Configuration.Services;

public class MenuService : CustomServiceBase
{
    public MenuService(IServiceCollection services) : base(services)
    {
        App.MapGet(Routing.MenuList, GetItemsAsync);
    }

    public ApiResultResponse<PaginatedItemResponse<MenuItemResponse>> GetItemsAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] int pageIndex = Config.DEFAULT_PAGE_INDEX,
        [FromQuery] int pageSize = Config.DEFAULT_PAGE_SIZE)
    {
        var response = new PaginatedItemResponse<MenuItemResponse>(pageIndex, pageSize, 0, new List<MenuItemResponse>());
        return Success(response);
    }
}

namespace MASA.Framework.Sdks.Authentication.Callers;

public class ConfigurationCaller : CallerBase
{
    protected override string BaseAddress { get; set; }

    public ConfigurationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(ConfigurationCaller);
        BaseAddress = configuration["ApiGateways:ConfigurationCaller"];
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>> GetItemsAsync(
        int pageIndex,
        int pageSize,
        string? name = null)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["name"] = name
        };
        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.MenuList, paramters);
            return (await CallerProvider.GetAsync<PaginatedItemResponse<MenuItemResponse>>(url))!;
        });
    }

    public async Task<ApiResultResponse<List<MenuItemResponse>>> GetAllAsync()
    {
        return (await ResultAsync(async () => await CallerProvider.GetAsync<List<MenuItemResponse>>(Routing.AllMenus)))!;
    }

    public async Task<ApiResultResponse<bool>> AnyChildAsync(Guid menuId)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["menuId"] = menuId.ToString(),
        };
        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.AnyChild, paramters);
            return await CallerProvider.GetAsync<bool>(url);
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(AddMenuRequest request)
    {
        return await ResultAsync(async () => await CallerProvider.PostAsync(Routing.OperateMenu, request));
    }

    public async Task<ApiResultResponseBase> EditAsync(EditMenuRequest request)
    {
        return await ResultAsync(async () => await CallerProvider.PutAsync(Routing.OperateMenu, request));
    }

    public async Task<ApiResultResponseBase> DeleteAsync(Guid id)
    {
        return await ResultAsync(async () => await CallerProvider.DeleteAsync(Routing.OperateMenu, new DeleteMenuRequest { MenuId = id }));
    }

    public async Task<ApiResultResponseBase> DeleteByIdsAsync(Guid[] menuIds)
    {
        return await ResultAsync(async () => await CallerProvider.DeleteAsync(Routing.OperateMenu, new DeleteMenuByIdsRequest(menuIds)));
    }
}

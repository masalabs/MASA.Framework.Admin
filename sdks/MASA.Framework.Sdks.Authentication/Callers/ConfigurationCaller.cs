namespace Masa.Framework.Sdks.Authentication.Callers;

public class ConfigurationCaller : CallerBase
{
    protected override string BaseAddress { get; set; }

    public ConfigurationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        BaseAddress = configuration["ApiGateways:ConfigurationCaller"];
    }

    protected override void UseHttpClientPost(MasaHttpClientBuilder masaHttpClientBuilder)
    {
        masaHttpClientBuilder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
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
            return (await Caller.GetAsync<PaginatedItemResponse<MenuItemResponse>>(url))!;
        });
    }

    public async Task<ApiResultResponse<List<MenuItemResponse>>> GetAllAsync()
    {
        return (await ResultAsync(async () => await Caller.GetAsync<List<MenuItemResponse>>(Routing.AllMenus)))!;
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
            return await Caller.GetAsync<bool>(url);
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(AddMenuRequest request)
    {
        return await ResultAsync(async () => await Caller.PostAsync(Routing.OperateMenu, request));
    }

    public async Task<ApiResultResponseBase> EditAsync(EditMenuRequest request)
    {
        return await ResultAsync(async () => await Caller.PutAsync(Routing.OperateMenu, request));
    }

    public async Task<ApiResultResponseBase> DeleteAsync(Guid id)
    {
        return await ResultAsync(async () => await Caller.DeleteAsync(Routing.OperateMenu, new DeleteMenuRequest { MenuId = id }));
    }

    public async Task<ApiResultResponseBase> DeleteByIdsAsync(Guid[] menuIds)
    {
        return await ResultAsync(async () => await Caller.DeleteAsync(Routing.DeleteMenuByIds, new DeleteMenuByIdsRequest(menuIds)));
    }
}

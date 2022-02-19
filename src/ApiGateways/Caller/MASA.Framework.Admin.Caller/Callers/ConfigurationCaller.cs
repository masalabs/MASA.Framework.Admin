namespace MASA.Framework.Admin.Caller.Callers;

public class ConfigurationCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public ConfigurationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(ConfigurationCaller);
        BaseAddress = configuration["ApiGateways:ConfigurationCaller"];
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>> GetItemsAsync(int pageIndex, int pageSize,string? name = null)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["name"] = name 
        };
        var url = QueryHelpers.AddQueryString(ConfigurationRouting.MenuList, paramters);
        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>>(url);
    }

    public async Task<ApiResultResponse<List<MenuItemResponse>>> GetAllAsync()
    {
        return await CallerProvider.GetAsync<ApiResultResponse<List<MenuItemResponse>>>(ConfigurationRouting.AllMenus);
    }

    public async Task<ApiResultResponse<bool>> AnyChildAsync(Guid menuId)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["menuId"] = menuId.ToString(),
        };
        var url = QueryHelpers.AddQueryString(ConfigurationRouting.AnyChild, paramters);
        return await CallerProvider.GetAsync<ApiResultResponse<bool>>(url);
    }

    public async Task<ApiResultResponseBase> CreateAsync(AddMenuRequest request)
    {
        return await CallerProvider.PostAsync<AddMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, request);
    }

    public async Task<ApiResultResponseBase> EditAsync(EditMenuRequest request)
    {
        return await CallerProvider.PutAsync<EditMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, request);
    }

    public async Task<ApiResultResponseBase> DeleteAsync(Guid id)
    {
        return await CallerProvider.DeleteAsync<DeleteMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, new DeleteMenuRequest { MenuId = id });
    }
}

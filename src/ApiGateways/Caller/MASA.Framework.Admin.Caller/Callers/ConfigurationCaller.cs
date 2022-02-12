namespace MASA.Framework.Admin.Caller.Callers;

public class ConfigurationCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public ConfigurationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(ConfigurationCaller);
        BaseAddress = configuration["ApiGateways:ConfigurationCaller"];
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>> GetItemsAsync(int pageIndex, int pageSize)
    {
        var paramters = new Dictionary<string, string>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
        };
        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>>(ConfigurationRouting.MenuList, paramters);
    }

    public async Task<ApiResultResponseBase> CreateAsync(AddMenuRequest request)
    {
        return await CallerProvider.PostAsync<AddMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, request);
    }

    public async Task<ApiResultResponseBase> EditAsync(EditMenuRequest request)
    {
        return await CallerProvider.PostAsync<EditMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, request);
    }

    public async Task<ApiResultResponseBase> DeleteAsync(Guid id)
    {
        return await CallerProvider.DeleteAsync<DeleteMenuRequest, ApiResultResponseBase>(ConfigurationRouting.OperateMenu, new DeleteMenuRequest { MenuId = id });
    }
}

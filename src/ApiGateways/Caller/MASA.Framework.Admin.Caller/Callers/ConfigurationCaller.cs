namespace MASA.Framework.Admin.Caller.Callers;

public class ConfigurationCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public ConfigurationCaller(IServiceProvider serviceProvider, IOptions<CallerOptions> options) : base(serviceProvider)
    {
        Name = nameof(ConfigurationCaller);
        BaseAddress = options.Value.ConfigurationCaller;
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>> GetItemsAsync(int pageIndex, int pageSize)
    {
        var paramters = new Dictionary<string, string>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
        };
        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<MenuItemResponse>>>(ConfigurationRouting.MenuList,
            paramters);
    }
}

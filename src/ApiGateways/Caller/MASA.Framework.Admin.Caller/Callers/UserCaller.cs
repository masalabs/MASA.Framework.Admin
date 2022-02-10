using MASA.Framework.Admin.Contracts.Users.Response;
using MASA.Utils.Caller.HttpClient;

namespace MASA.Framework.Admin.Caller.Callers;

public class UserCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5136/api";

    public UserCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }

    public async Task<List<UserItemResponse>> List(int pageIndex = 1, int pageSize = 20, string account = "", int state = -1)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "brandId", brandId.ToString() },
            { "typeId", typeId.ToString() },
            { "pageIndex", pageIndex.ToString() },
            { "pageSize", pageSize.ToString() }
        };
        var url = QueryHelpers.AddQueryString(_getCatalogItemsUrl, queryArguments);
        return await CallerProvider.GetAsync<List<UserItemResponse>>($"/users/items");
    }

    public async Task<UserDetailResponse> Details(string id)
    {
        return await CallerProvider.GetAsync<UserDetailResponse>($"/users/{id}");
    }
}


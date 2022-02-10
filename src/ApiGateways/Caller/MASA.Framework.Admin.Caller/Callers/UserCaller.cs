using MASA.Framework.Admin.Contracts.User;
using MASA.Framework.Admin.Contracts.User.Response;
using MASA.Utils.Caller.HttpClient;
using Microsoft.AspNetCore.WebUtilities;

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
            { "pageIndex", pageIndex.ToString() },
            { "pageSize", pageSize.ToString() },
            { "account", pageIndex.ToString() },
            { "state", state.ToString() }
        };
        var url = QueryHelpers.AddQueryString(Routing.UserList, queryArguments);
        return await CallerProvider.GetAsync<List<UserItemResponse>>(url);
    }

    public async Task<UserDetailResponse> Details(string id)
    {
        return await CallerProvider.GetAsync<UserDetailResponse>(String.Format(Routing.UserDetail, id));
    }
}


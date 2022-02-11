using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Contracts.User;
using MASA.Framework.Admin.Contracts.User.Response;
using MASA.Utils.Caller.HttpClient;
using Microsoft.AspNetCore.WebUtilities;

namespace MASA.Framework.Admin.Caller.Callers;

public class UserCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5136";

    public UserCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Name = nameof(UserCaller);
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>> GetListAsync(int pageIndex = 1, int pageSize = 20, string account = "", int state = -1)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "pageIndex", pageIndex.ToString() },
            { "pageSize", pageSize.ToString() },
            { "account", pageIndex.ToString() },
            { "state", state.ToString() }
        };
        var url = QueryHelpers.AddQueryString(Routing.UserList, queryArguments);
        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>>(url);
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetDetailsAsync(string id)
    {
        return await CallerProvider.GetAsync<ApiResultResponse<UserDetailResponse>>(String.Format(Routing.UserDetail, id));
    }

    public async Task<ApiResultResponseBase> CreateAsync(string id, string name)
    {
        return await CallerProvider.PostAsync<string, ApiResultResponseBase>(Routing.OperateUser, "");
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await CallerProvider.DeleteAsync<object, ApiResultResponseBase>(Routing.OperateUser, new { id });
    }
}


namespace MASA.Framework.Admin.Caller.Callers;

public class UserCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public UserCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(UserCaller);
        BaseAddress = configuration["ApiGateways:UserCaller"];
    }

    protected override IHttpClientBuilder UseHttpClient()
    {
        return base.UseHttpClient().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>> GetListAsync(int pageIndex = 1, int pageSize = 20,
        string account = "", int state = -1)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "pageIndex", pageIndex.ToString() },
            { "pageSize", pageSize.ToString() },
            { "account", pageIndex.ToString() },
            { "state", state.ToString() }
        };
        var url = QueryHelpers.AddQueryString(Contracts.User.Routing.UserList, queryArguments);
        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>>(url);
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetDetailsAsync(string id)
    {
        var url = UserRouting.UserDetail.Replace($"{{{nameof(id)}}}", id);
        return await CallerProvider.GetAsync<ApiResultResponse<UserDetailResponse>>(url);
    }

    public async Task<ApiResultResponseBase> CreateAsync(UserCreateRequest userCreateRequest)
    {
        return await CallerProvider.PostAsync<UserCreateRequest, ApiResultResponseBase>(UserRouting.OperateUser, userCreateRequest);
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await CallerProvider.DeleteAsync<object, ApiResultResponseBase>(UserRouting.OperateUser, new { id });
    }
}

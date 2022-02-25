namespace MASA.Framework.Sdks.Authentication.Callers;

public class UserCaller : CallerBase
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
            { "account", account },
            { "state", state.ToString() }
        };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.UserList, queryArguments);
            var response = await CallerProvider.GetAsync<PaginatedItemResponse<UserItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserRoleResponse>>> GetUserRolesAsync(Guid userId)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "userId", userId.ToString() }
        };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.UserRole, queryArguments);
            var response = await CallerProvider.GetAsync<List<UserRoleResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetDetailsAsync(string id)
    {
        var url = Routing.UserDetail.Replace($"{{{nameof(id)}}}", id);
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<UserDetailResponse>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateUserRequest userCreateRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.OperateUser, userCreateRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateRoleAsync(CreateUserRoleRequest createUserRoleRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.UserRole, createUserRoleRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> RemoveUserRoleAsync(RemoveUserRoleRequest removeUserRoleRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.DeleteAsync(Routing.UserRole, removeUserRoleRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateUserGroupAsync(CreateUserGroupRequest createUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.UserGroup, createUserGroupRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> RemoveUserGroupAsync(RemoveUserGroupRequest removeUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.DeleteAsync(Routing.UserGroup, removeUserGroupRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.DeleteAsync(Routing.OperateUser, new { id });
            return response!;
        });
    }

    public async Task<ApiResultResponse<string>> LoginAsync(string account, string password)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync<UserLoginRequest, string>(Routing.UserLogin, new UserLoginRequest { Account = account, Password = password });
            return response!;
        });
    }

    public async Task<ApiResultResponse<UserStatisticResponse>> GetUserStatisticAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<UserStatisticResponse>("");
            return response!;
        });
    }
}


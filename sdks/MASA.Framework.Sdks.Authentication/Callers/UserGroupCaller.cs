namespace Masa.Framework.Sdks.Authentication.Callers;

public class UserGroupCaller : CallerBase
{

    protected override string BaseAddress { get; set; } = "";

    public UserGroupCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(UserGroupCaller);
        BaseAddress = configuration["ApiGateways:UserCaller"];
    }

    protected override IHttpClientBuilder UseHttpClient()
    {
        return base.UseHttpClient().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<UserGroupItemResponse>>> GetListAsync(int pageIndex = 1, int pageSize = 20,
        string name = "")
    {
        var queryArguments = new Dictionary<string, string?>()
            {
                { "pageIndex", pageIndex.ToString() },
                { "pageSize", pageSize.ToString() },
                { "name", name }
            };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.UserGroupList, queryArguments);
            var response = await CallerProvider.GetAsync<PaginatedItemResponse<UserGroupItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateGroupRequest createUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.OperateGroup, createUserGroupRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.DeleteAsync(Routing.OperateGroup, new { id });
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserGroupItemResponse>>> GetUserGroupsAsync(Guid userId)
    {
        var queryArguments = new Dictionary<string, string>()
        {
            { "userId", userId.ToString() }
        };
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserGroupItemResponse>>(Routing.GroupByUser, queryArguments);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserItemResponse>>> GetUsersAsync(Guid groupId)
    {
        var queryArguments = new Dictionary<string, string>()
        {
            { "groupId", groupId.ToString() }
        };
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserItemResponse>>(Routing.GroupUsers, queryArguments);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserGroupItemResponse>>> SelectUserGroupsAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserGroupItemResponse>>(Routing.UserGroupSelect);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> AddUserAsync(CreateUserGroupRequest createUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.OperateGroup, createUserGroupRequest);
            return response!;
        });
    }

}


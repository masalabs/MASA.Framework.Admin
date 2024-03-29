namespace Masa.Framework.Sdks.Authentication.Callers;

public class UserGroupCaller : CallerBase
{
    AuthenticationCaller _authenticationCaller;

    protected override string BaseAddress { get; set; }

    public UserGroupCaller(AuthenticationCaller authenticationCaller, IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        BaseAddress = configuration["ApiGateways:UserCaller"];
        _authenticationCaller = authenticationCaller;
    }

    protected override void UseHttpClientPost(MasaHttpClientBuilder masaHttpClientBuilder)
    {
        masaHttpClientBuilder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
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
            var response = await Caller.GetAsync<PaginatedItemResponse<UserGroupItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateGroupRequest createUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.PostAsync(Routing.OperateGroup, createUserGroupRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.DeleteAsync(Routing.OperateGroup, new { id });
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
            var response = await Caller.GetAsync<List<UserGroupItemResponse>>(Routing.GroupByUser, queryArguments);
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
            var response = await Caller.GetAsync<List<UserItemResponse>>(Routing.GroupUsers, queryArguments);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserGroupItemResponse>>> SelectUserGroupsAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.GetAsync<List<UserGroupItemResponse>>(Routing.UserGroupSelect);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> AddUserAsync(CreateUserGroupRequest createUserGroupRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.PostAsync(Routing.OperateGroup, createUserGroupRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<Guid>>> GetPermissionIdsAsync(Guid groupId)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "groupId", groupId.ToString() }
        };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.UserGroupPermissions, queryArguments);
            var response = await Caller.GetAsync<List<Guid>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<PermissionItemResponse>>> GetPermissionsByUserIdAsync(Guid userId)
    {
        var permissions = new List<PermissionItemResponse>();
        var groupsReponse = await GetUserGroupsAsync(userId);
        if (groupsReponse.Success is true)
        {
            var permissionIds = new List<Guid>();
            foreach (var group in groupsReponse.Data!)
            {
                var groupPermissionIds = (await GetPermissionIdsAsync(group.Id))?.Data ?? new();
                permissionIds.AddRange(groupPermissionIds);
                var groupPermissions = (await _authenticationCaller.GetPermissionsByIds(permissionIds))?.Data ?? new();
                permissions.AddRange(groupPermissions.Where(p => p.Enable));
            }
        }
        return ApiResultResponse<List<PermissionItemResponse>>.ResponseSuccess(permissions, "Success");
    }

    public async Task<ApiResultResponseBase> RemovePermissionAsync(RemoveGroupPermissionRequest removeGroupPermissionRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.DeleteAsync(Routing.GroupPermission, removeGroupPermissionRequest);
            return response!;
        });
    }
}


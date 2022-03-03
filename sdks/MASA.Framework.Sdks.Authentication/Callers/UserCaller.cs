namespace Masa.Framework.Sdks.Authentication.Callers;

public class UserCaller : CallerBase
{
    AuthenticationCaller _authenticationCaller;
    UserGroupCaller _userGroupCaller;

    protected override string BaseAddress { get; set; }

    public UserCaller(AuthenticationCaller authenticationCaller,IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(UserCaller);
        BaseAddress = configuration["ApiGateways:UserCaller"];
        _authenticationCaller = authenticationCaller;
        _userGroupCaller = new UserGroupCaller(authenticationCaller, serviceProvider, configuration);
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

    public async Task<ApiResultResponse<List<UserItemResponse>>> GetAllUserAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserItemResponse>>(Routing.AllUser);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<UserItemResponse>>> GetUserListByRoleIdAsync(Guid roleId)
    {
        var queryArguments = new Dictionary<string, string>()
        {
            { "roleId", roleId.ToString() }
        };

        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserItemResponse>>(Routing.UserListByRole, queryArguments);
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

    public async Task<ApiResultResponseBase> CreateUserRolesAsync(CreateUserRolesRequest request)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.UserRoles, request);
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

    public async Task<ApiResultResponse<List<UserItemResponse>>> GetUsersWithDepartmentAsync(Guid departmentId, bool all = false)
    {
        var queryArguments = new Dictionary<string, string>()
        {
            { "departmentId", departmentId.ToString() },
            { "all", all.ToString() }
        };
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<UserItemResponse>>(Routing.UserWithDepartment, queryArguments);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.DeleteAsync(Routing.OperateUser, new { UserId = id });
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

    public async Task<ApiResultResponse<List<AuthorizeItemResponse>>> GetAuthorizeByUserAsync(Guid userId)
    {
        var userRolesResponse = await GetUserRolesAsync(userId);
        if (!userRolesResponse.Success) return ApiResultResponse<List<AuthorizeItemResponse>>.ResponseLose(userRolesResponse.Message, null);
        else
        {
            if (userRolesResponse.Data?.Count > 0)
            {
                var groupPermissionsReponse = await _userGroupCaller.GetPermissionsByUserIdAsync(userId);
                if (groupPermissionsReponse.Success is false) return ApiResultResponse<List<AuthorizeItemResponse>>.ResponseLose(groupPermissionsReponse.Message, null);
                var rolePermissionsReponse = await _authenticationCaller.GetPermissionsByRolesAsync(userRolesResponse.Data!.Select(ur => ur.RoleId).ToList());
                if (rolePermissionsReponse.Success is false) return rolePermissionsReponse;

                rolePermissionsReponse.Data!.AddRange(groupPermissionsReponse.Data!.Select(p => new AuthorizeItemResponse()
                {
                    PermissionId = p.Id,PermissionName = p.Name,
                    ObjectType = (ObjectType)p.ObjectType,
                    Resource=p.Resource,
                    Scope = p.Scope,
                    Action = p.Action,
                    PermissionType = (PermissionType)p.PermissionType
                }));
                return rolePermissionsReponse;
            }
            else return ApiResultResponse<List<AuthorizeItemResponse>>.ResponseSuccess(new(), "success");
        }
    }
}


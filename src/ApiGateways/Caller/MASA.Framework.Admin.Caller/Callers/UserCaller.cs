using MASA.Framework.Admin.Contracts.User.Request;

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
        try
        {
            var url = QueryHelpers.AddQueryString(Contracts.User.Routing.UserList, queryArguments);
            return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>>(url);
        }
        catch (Exception)
        {
            return new ApiResultResponse<PaginatedItemResponse<UserItemResponse>>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }
    }

    public async Task<ApiResultResponse<List<UserRoleResponse>>> GetUserRolesAsync(Guid userId)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "userId", userId.ToString() }
        };
        try
        {
            var url = QueryHelpers.AddQueryString(Contracts.User.Routing.UserRole, queryArguments);
            return await CallerProvider.GetAsync<ApiResultResponse<List<UserRoleResponse>>>(url);
        }
        catch (Exception)
        {
            return new ApiResultResponse<List<UserRoleResponse>>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }
    }

    public async Task<ApiResultResponse<UserDetailResponse>> GetDetailsAsync(string id)
    {
        var url = UserRouting.UserDetail.Replace($"{{{nameof(id)}}}", id);
        try
        {
            return await CallerProvider.GetAsync<ApiResultResponse<UserDetailResponse>>(url);
        }
        catch (Exception)
        {
            return new ApiResultResponse<UserDetailResponse>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }

    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateUserRequest userCreateRequest)
    {
        try
        {
            return await CallerProvider.PostAsync<CreateUserRequest, ApiResultResponseBase>(UserRouting.OperateUser, userCreateRequest);
        }
        catch (Exception)
        {
            return new ApiResultResponse<ApiResultResponseBase>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }
    }

    public async Task<ApiResultResponseBase> CreateRoleAsync(CreateUserRoleRequest createUserRoleRequest)
    {
        try
        {
            return await CallerProvider.PostAsync<CreateUserRoleRequest, ApiResultResponseBase>(UserRouting.UserRole, createUserRoleRequest);
        }
        catch (Exception)
        {
            return new ApiResultResponse<ApiResultResponseBase>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }
    }

    public async Task<ApiResultResponseBase> DeleteAsync(string id)
    {
        try
        {
            return await CallerProvider.DeleteAsync<object, ApiResultResponseBase>(UserRouting.OperateUser, new { id });
        }
        catch (Exception)
        {
            return new ApiResultResponse<ApiResultResponseBase>(null)
            {
                Success = false,
                Code = Code.SYSTEM_ERROR
            };
        }
    }
}

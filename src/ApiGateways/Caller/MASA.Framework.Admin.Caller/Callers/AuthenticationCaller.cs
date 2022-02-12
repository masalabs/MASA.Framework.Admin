using System.Text.Json;

namespace MASA.Framework.Admin.Caller.Callers;

public class AuthenticationCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public AuthenticationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(AuthenticationCaller);
        BaseAddress = configuration["ApiGateways:AuthenticationCaller"];
    }

    #region Object

    public async Task<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>> GetObjectItemsAsync(int pageIndex, int pageSize,
        int type = -1, string? name = null)
    {
        var paramters = new Dictionary<string, string>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["type"] = type.ToString(),
            ["name"] = name ?? "",
        };

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>>(AuthenticationRouting.ObjectList,
            paramters);
    }

    public async Task<ApiResultResponseBase> AddObjectAsync(AddObjectRequest request)
    {
        return await CallerProvider.PostAsync<AddObjectRequest, ApiResultResponseBase>(AuthenticationRouting.OperateObject, request);
    }

    public async Task<ApiResultResponseBase> EditObjectAsync(EditObjectRequest request)
    {
        return await CallerProvider.PostAsync<EditObjectRequest, ApiResultResponseBase>(AuthenticationRouting.OperateObject, request);
    }

    // public async Task<ApiResultResponseBase> ChangeObjectStateAsync(ChangeStateCommand command)
    // {
    //     return await CallerProvider.PostAsync<ChangeStateCommand, ApiResultResponseBase>(AuthenticationRouting.OperateObject, command);
    // }

    #endregion

    #region Role

    public async Task<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>> GetRoleItemsAsync(int pageIndex, int pageSize,
        int state = -1, string? name = null)
    {
        var paramters = new Dictionary<string, string>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["state"] = state.ToString(),
            ["name"] = name ?? "",
        };

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>>(AuthenticationRouting.RoleList, paramters);
    }

    public async Task<ApiResultResponseBase> AddRoleAsync(AddRoleRequest request)
    {
        return await CallerProvider.PostAsync<AddRoleRequest, ApiResultResponseBase>(AuthenticationRouting.OperateRole, request);
    }

    public async Task<ApiResultResponseBase> EditRoleAsync(EditRoleRequest request)
    {
        return await CallerProvider.PostAsync<EditRoleRequest, ApiResultResponseBase>(AuthenticationRouting.OperateRole, request);
    }

    public async Task<ApiResultResponse<List<RoleItemResponse>>> SelectRoleAsync()
    {
        return await CallerProvider.GetAsync<ApiResultResponse<List<RoleItemResponse>>>(AuthenticationRouting.RoleSelect);
    }

    public async Task<ApiResultResponse<List<RoleItemResponse>>> GetRolesByIdsAsync(List<Guid> roleIds)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "roleIds", JsonSerializer.Serialize(roleIds)}
        };
        var url = QueryHelpers.AddQueryString(AuthenticationRouting.RoleListByIds, queryArguments);
        return await CallerProvider.GetAsync<ApiResultResponse<List<RoleItemResponse>>>(url);
    }

    #endregion

    #region Authorize

    public async Task<ApiResultResponse<List<AuthorizeItemResponse>>> GetAuthorizeItemsAsync(Guid roleId)
    {
        var paramters = new Dictionary<string, string>
        {
            ["roleId"] = roleId.ToString(),
        };

        return await CallerProvider.GetAsync<ApiResultResponse<List<AuthorizeItemResponse>>>(AuthenticationRouting.AuthorizeList,
            paramters);
    }

    #endregion
}

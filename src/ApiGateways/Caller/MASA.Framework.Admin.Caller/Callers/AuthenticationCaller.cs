namespace MASA.Framework.Admin.Caller.Callers;

public class AuthenticationCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public AuthenticationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(AuthenticationCaller);
        BaseAddress = configuration["ApiGateways.AuthenticationCaller"];
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

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>>(Routing.ObjectList,
            paramters);
    }

    public async Task<ApiResultResponseBase> AddObjectAsync(AddObjectRequest request)
    {
        return await CallerProvider.PostAsync<AddObjectRequest, ApiResultResponseBase>(Routing.OperateObject, request);
    }

    public async Task<ApiResultResponseBase> EditObjectAsync(EditObjectRequest request)
    {
        return await CallerProvider.PostAsync<EditObjectRequest, ApiResultResponseBase>(Routing.OperateObject, request);
    }

    // public async Task<ApiResultResponseBase> ChangeObjectStateAsync(ChangeStateCommand command)
    // {
    //     return await CallerProvider.PostAsync<ChangeStateCommand, ApiResultResponseBase>(Routing.OperateObject, command);
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

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>>(Routing.RoleList, paramters);
    }

    public async Task<ApiResultResponseBase> AddRoleAsync(AddRoleRequest request)
    {
        return await CallerProvider.PostAsync<AddRoleRequest, ApiResultResponseBase>(Routing.OperateRole, request);
    }

    public async Task<ApiResultResponseBase> EditRoleAsync(EditRoleRequest request)
    {
        return await CallerProvider.PostAsync<EditRoleRequest, ApiResultResponseBase>(Routing.OperateRole, request);
    }

    #endregion
}

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

    protected override IHttpClientBuilder UseHttpClient()
    {
        return base.UseHttpClient().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }

    #region Object

    public async Task<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>> GetObjectItemsAsync(int pageIndex, int pageSize,
        int type = -1, string? name = null)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["type"] = type.ToString(),
            ["name"] = name,
        };
        var url = QueryHelpers.AddQueryString(AuthenticationRouting.ObjectList, paramters);

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>>(url);
    }

    public async Task<ApiResultResponse<List<ObjectItemResponse>>> GetAllAsync()
    {
        return await CallerProvider.GetAsync<ApiResultResponse<List<ObjectItemResponse>>>(AuthenticationRouting.ObjectAll);
    }

    public async Task<ApiResultResponse<bool>> ContainsObjectAsync(Guid objectId,string code)
    {
        var paramters = new Dictionary<string, string?>
        {
            ["objectId"] = objectId.ToString(),
            ["code"] = code,
        };
        var url = QueryHelpers.AddQueryString(AuthenticationRouting.ContainsObject, paramters);

        return await CallerProvider.GetAsync<ApiResultResponse<bool>>(url);
    }


    public async Task<ApiResultResponseBase> AddObjectAsync(AddObjectRequest request)
    {
        return await CallerProvider.PostAsync<AddObjectRequest, ApiResultResponseBase>(AuthenticationRouting.OperateObject, request);
    }

    public async Task<ApiResultResponseBase> EditObjectAsync(EditObjectRequest request)
    {
        return await CallerProvider.PutAsync<EditObjectRequest, ApiResultResponseBase>(AuthenticationRouting.OperateObject, request);
    }

    public async Task<ApiResultResponseBase> DeleteObjectAsync(DeleteObjectRequest request)
    {
        return await CallerProvider.DeleteAsync<DeleteObjectRequest, ApiResultResponseBase>(AuthenticationRouting.OperateObject, request);
    }

    public async Task<ApiResultResponseBase> BatchDeleteObjectAsync(BatchDeleteObjectRequest request)
    {
        return await CallerProvider.DeleteAsync<BatchDeleteObjectRequest, ApiResultResponseBase>(AuthenticationRouting.BatchDeleteObject, request);
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
        var paramters = new Dictionary<string, string?>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["state"] = state.ToString(),
            ["name"] = name ,
        };
        var url = QueryHelpers.AddQueryString(AuthenticationRouting.RoleList, paramters);

        return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>>(url);
    }

    public async Task<ApiResultResponseBase> AddRoleAsync(AddRoleRequest request)
    {
        return await CallerProvider.PostAsync<AddRoleRequest, ApiResultResponseBase>(AuthenticationRouting.OperateRole, request);
    }

    public async Task<ApiResultResponseBase> EditRoleAsync(EditRoleRequest request)
    {
        return await CallerProvider.PutAsync<EditRoleRequest, ApiResultResponseBase>(AuthenticationRouting.OperateRole, request);
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

    public async Task<ApiResultResponse<RoleDetailResponse>> GetRoleByIdAsync(Guid roleId)
    {
        var queryArguments = new Dictionary<string, string?>()
        {
           ["roleId"]=roleId.ToString()
        };
        var url = QueryHelpers.AddQueryString(AuthenticationRouting.RoleDetail, queryArguments);
        return await CallerProvider.GetAsync<ApiResultResponse<RoleDetailResponse>>(url);
    }

    public async Task<ApiResultResponseBase> DeleteRoleAsync(DeleteRoleRequest request)
    {
        return await CallerProvider.DeleteAsync<DeleteRoleRequest, ApiResultResponseBase>(AuthenticationRouting.OperateRole, request);
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

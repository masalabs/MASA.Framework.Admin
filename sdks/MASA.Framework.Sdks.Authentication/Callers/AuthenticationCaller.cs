namespace MASA.Framework.Sdks.Authentication.Callers;

public class AuthenticationCaller : CallerBase
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

    #region Role

    public async Task<ApiResultResponse<PaginatedItemResponse<RoleItemResponse>>?> GetRoleItemsAsync(int pageIndex, int pageSize,
        int state = -1, string? name = null)
    {
        var paramters = new Dictionary<string, string>
        {
            ["pageIndex"] = pageIndex.ToString(),
            ["pageSize"] = pageSize.ToString(),
            ["state"] = state.ToString(),
            ["name"] = name ?? "",
        };

        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<PaginatedItemResponse<RoleItemResponse>>(Routing.RoleList, paramters);
            return response!;
        });
    }

    public async Task<ApiResultResponse<RoleDetailResponse>> GetRoleDetailAsync(Guid id)
    {
        return await ResultAsync(async () =>
        {
            var paramters = new Dictionary<string, string>
            {
                ["id"] = id.ToString(),
            };
            var response = await CallerProvider.GetAsync<RoleDetailResponse>(Routing.RoleDetail, paramters);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> AddRoleAsync(AddRoleRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PostAsync(Routing.OperateRole, request);
        });
    }

    public async Task<ApiResultResponseBase> AddChildrenRolesAsync(AddChildRolesRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PostAsync(Routing.AddChildRoles, request);
        });
    }

    public async Task<ApiResultResponseBase> EditRoleAsync(EditRoleRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PutAsync(Routing.OperateRole, request);
        });
    }

    public async Task<ApiResultResponse<List<RoleItemResponse>>> SelectRoleAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<RoleItemResponse>>(Routing.RoleSelect);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<RoleItemsResponse>>> GetAllRoleItemAsync()
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.GetAsync<List<RoleItemsResponse>>(Routing.AllRoleItem);
            return response!;
        });
    }

    public async Task<ApiResultResponse<List<RoleItemResponse>>> GetRolesByIdsAsync(List<Guid> roleIds)
    {
        return (await ResultAsync(async () =>
        {
            var queryArguments = new Dictionary<string, string?>()
            {
                { "roleIds", JsonSerializer.Serialize(roleIds) }
            };
            return await CallerProvider.GetAsync<List<RoleItemResponse>>(Routing.RoleListByIds, queryArguments!);
        }))!;
    }

    public async Task<ApiResultResponseBase> DeleteRoleAsync(DeleteRoleRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.DeleteAsync(Routing.OperateRole, request);
        });
    }

    #endregion
}

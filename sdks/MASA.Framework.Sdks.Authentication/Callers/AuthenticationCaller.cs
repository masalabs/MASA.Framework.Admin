namespace MASA.Framework.Sdks.Authentication.Callers;

public class AuthenticationCaller : CallerBase
{
    protected override string BaseAddress { get; set; }

    public AuthenticationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(AuthenticationCaller);
        BaseAddress = configuration["ApiGateways:AuthenticationCaller"];
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
            var url = Routing.RoleDetail.Replace("{id}", id.ToString());
            var response = await CallerProvider.GetAsync<RoleDetailResponse>(url);
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

    public async Task<ApiResultResponseBase> EditRoleAsync(EditRoleRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PostAsync(Routing.OperateRole, request);
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

    #region Object

    public async Task<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>> GetObjectItemsAsync(int pageIndex, int pageSize,
        int type = -1, string? name = null)
    {
        return (await ResultAsync(async () =>
        {
            var paramters = new Dictionary<string, string?>
            {
                ["pageIndex"] = pageIndex.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["type"] = type.ToString(),
                ["name"] = name,
            };
            return await CallerProvider.GetAsync<PaginatedItemResponse<ObjectItemResponse>>(Routing.ObjectList, paramters!);
        }))!;
    }

    public async Task<ApiResultResponse<List<ObjectItemResponse>>> GetAllAsync()
    {
        return (await ResultAsync(async () => await CallerProvider.GetAsync<List<ObjectItemResponse>>(Routing.ObjectAll)))!;
    }

    public async Task<ApiResultResponse<bool>> ContainsObjectAsync(Guid objectId, string code)
    {
        return await ResultAsync(async () =>
        {
            var paramters = new Dictionary<string, string>
            {
                ["objectId"] = objectId.ToString(),
                ["code"] = code
            };
            return await CallerProvider.GetAsync<bool>(Routing.ContainsObject, paramters);
        });
    }

    public async Task<ApiResultResponseBase> AddObjectAsync(AddObjectRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PostAsync(Routing.OperateObject, request);
        });
    }

    public async Task<ApiResultResponseBase> EditObjectAsync(EditObjectRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.PutAsync(Routing.OperateObject, request);
        });
    }

    public async Task<ApiResultResponseBase> DeleteObjectAsync(DeleteObjectRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.DeleteAsync(Routing.OperateObject, request);
        });
    }

    public async Task<ApiResultResponseBase> BatchDeleteObjectAsync(BatchDeleteObjectRequest request)
    {
        return await ResultAsync(async () =>
        {
            await CallerProvider.DeleteAsync(Routing.BatchDeleteObject,
                request);
        });
    }

    #endregion
}

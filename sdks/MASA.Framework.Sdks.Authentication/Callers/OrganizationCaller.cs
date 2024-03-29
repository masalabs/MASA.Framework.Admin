namespace Masa.Framework.Sdks.Authentication.Callers;

public class OrganizationCaller : CallerBase
{

    protected override string BaseAddress { get; set; }

    public OrganizationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        BaseAddress = configuration["ApiGateways:UserCaller"];
    }

    protected override void UseHttpClientPost(MasaHttpClientBuilder masaHttpClientBuilder)
    {
        masaHttpClientBuilder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }

    public async Task<ApiResultResponse<List<DepartmentItemResponse>>> GetListAsync(Guid parentId = default(Guid))
    {
        var queryArguments = new Dictionary<string, string?>()
            {
                { "parentId", parentId.ToString() }
            };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.DepartmentList, queryArguments);
            var response = await Caller.GetAsync<List<DepartmentItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateDepartmentRequest createDepartmentRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.PostAsync(Routing.OperateDepartment, createDepartmentRequest);
            return response!;
        });
    }

    public async Task<ApiResultResponse<PaginatedItemResponse<UserItemResponse>>> GetDepartmentUsersAsync(int pageIndex = 1, int pageSize = 20, Guid departmentId = default(Guid))
    {
        var queryArguments = new Dictionary<string, string?>()
        {
            { "pageIndex", pageIndex.ToString() },
            { "pageSize", pageSize.ToString() },
            { "departmentId", departmentId.ToString() }
        };

        return await ResultAsync(async () =>
        {
            var url = QueryHelpers.AddQueryString(Routing.DepartmentUsers, queryArguments);
            var response = await Caller.GetAsync<PaginatedItemResponse<UserItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> UpdateDepartmentUsers(UpdateDepartmentUserRequest updateDepartmentUserRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await Caller.PostAsync(Routing.DepartmentUsers, updateDepartmentUserRequest);
            return response!;
        });
    }

}


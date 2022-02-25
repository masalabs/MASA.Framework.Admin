namespace MASA.Framework.Sdks.Authentication.Callers;

public class OrganizationCaller : CallerBase
{

    protected override string BaseAddress { get; set; }

    public OrganizationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
    {
        Name = nameof(OrganizationCaller);
        BaseAddress = configuration["ApiGateways:UserCaller"];
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
            var response = await CallerProvider.GetAsync<List<DepartmentItemResponse>>(url);
            return response!;
        });
    }

    public async Task<ApiResultResponseBase> CreateAsync(CreateDepartmentRequest createDepartmentRequest)
    {
        return await ResultAsync(async () =>
        {
            var response = await CallerProvider.PostAsync(Routing.OperateDepartment, createDepartmentRequest);
            return response!;
        });
    }

}


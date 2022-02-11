using MASA.Framework.Admin.Contracts.Authentication;
using MASA.Framework.Admin.Contracts.Authentication.Const;
using MASA.Framework.Admin.Contracts.Authentication.Enum;
using MASA.Framework.Admin.Contracts.Authentication.Response;
using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Utils.Caller.HttpClient;
using Microsoft.Extensions.Configuration;

namespace MASA.Framework.Admin.Caller.Callers
{
    public class AuthenticationCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; }

        public AuthenticationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            Name = nameof(ConfigurationCaller);
            BaseAddress = configuration["ApiGateways.AuthenticationCaller"];
        }

        public async Task<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>> GetObjectItemsAsync(int pageIndex, int pageSize, int type = -1, string name = "")
        {
            var paramters = new Dictionary<string, string>
            {
                ["pageIndex"] = pageIndex.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["type"] = type.ToString(),
                ["name"] = name,
            };

            return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemResponse<ObjectItemResponse>>>(Routing.ObjectList, paramters);
        }
    }
}

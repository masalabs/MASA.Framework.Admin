using MASA.Framework.Admin.Contracts.Authentication.Enum;
using MASA.Framework.Admin.Contracts.Authentication.Response;
using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Contracts.Configuration.Const;
using MASA.Framework.Admin.Contracts.Configuration.Response;
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

        public async Task<ApiResultResponse<PaginatedItemsViewModel<ObjectItemResponse>>> GetItemsAsync(int pageIndex, int pageSize, ObjectType? type = null, string name = "")
        {
            var paramters = new Dictionary<string, string>
            {
                ["pageIndex"] = pageIndex.ToString(),
                ["pageSize"] = pageSize.ToString(),
                ["name"] = name,
            };

            return await CallerProvider.GetAsync<ApiResultResponse<PaginatedItemsViewModel<MenuItemResponse>>>(UrlRule.MENU_SERVICE, paramters);
        }
    }
}

using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Contracts.Configuration.Response;
using MASA.Utils.Caller.HttpClient;
using Microsoft.Extensions.Configuration;

namespace MASA.Framework.Admin.Caller.Callers
{
    public class ConfigurationCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; }

        public ConfigurationCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            Name = nameof(ConfigurationCaller);
            BaseAddress = configuration["ApiGateways.ConfigurationCaller"];
        }

        public async Task<PaginatedItemsViewModel<MenuItemResponse>> GetItemsAsync(int pageIndex, int pageSize)
        {
            var paramters = new Dictionary<string, string>
            {
                ["pageIndex"] = pageIndex.ToString(),
                ["pageSize"] = pageSize.ToString(),
            };
            return await CallerProvider.GetAsync<PaginatedItemsViewModel<MenuItemResponse>>($"/api/configurations/menu/items", paramters);
        }
    }
}

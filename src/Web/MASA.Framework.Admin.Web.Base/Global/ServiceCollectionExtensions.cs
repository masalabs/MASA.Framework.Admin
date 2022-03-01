using System.Net.Http.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGlobalForServer(this IServiceCollection services)
        {     
            services.AddMasaI18nForServer("wwwroot/i18n");
            services.AddScoped<NavHelper>();
            services.AddScoped<GlobalConfig>();
            services.AddScoped<PermissionHelper>();

            return services;
        }

        public static async Task<IServiceCollection> AddGlobalForWasmAsync(this IServiceCollection services, string baseUri)
        {          
            await services.AddMasaI18nForWasmAsync(Path.Combine(baseUri, $"i18n"));
            services.AddScoped<NavHelper>();
            services.AddScoped<GlobalConfig>();
            services.AddScoped<PermissionHelper>();

            return services;
        }
    }
}

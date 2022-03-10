using Masa.Framework.Admin.Rcl.Rbac;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRBAC(this IServiceCollection services)
        {
            services.AddScoped<MenuPage>();
            services.AddScoped<PermissionPage>();
            services.AddScoped<RolePage>();
            services.AddScoped<RoleDetailsPage>();
            Masa.Framework.Sdks.Authentication.ServiceCollectionExtensions.AddAuthentication(services);

            return services;
        }
    }
}

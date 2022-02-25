using Masa.Framework.Admin.RCL.RBAC;

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
            MASA.Framework.Sdks.Authentication.ServiceCollectionExtensions.AddAuthentication(services);

            return services;
        }
    }
}

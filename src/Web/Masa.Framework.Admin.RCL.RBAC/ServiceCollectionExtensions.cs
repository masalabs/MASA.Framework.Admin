using Masa.Framework.Admin.RCL.RBAC;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRBAC(this IServiceCollection services)
        {
            services.AddScoped<MenuPage>();
            services.AddScoped<ObjectPage>();
            services.AddScoped<RolePage>();
            MASA.Framework.Sdks.Authentication.ServiceCollectionExtensions.AddAuthentication(services);
            return services;
        }
    }
}

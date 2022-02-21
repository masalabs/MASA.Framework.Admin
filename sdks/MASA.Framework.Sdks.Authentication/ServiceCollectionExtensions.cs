namespace MASA.Framework.Sdks.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddScoped<IClient, DefaultClient>();
        return services;
    }
}

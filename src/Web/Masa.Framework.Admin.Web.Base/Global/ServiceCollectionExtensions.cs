namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IBlazorComponentBuilder AddGlobalForServer(this IBlazorComponentBuilder blazorComponentBuilder)
        {
            blazorComponentBuilder.AddI18nForServer("wwwroot/i18n");
            blazorComponentBuilder.Services.AddScoped<NavHelper>();
            blazorComponentBuilder.Services.AddScoped<GlobalConfig>();
            blazorComponentBuilder.Services.AddScoped<PermissionHelper>();

            return blazorComponentBuilder;
        }

        public static async Task<IBlazorComponentBuilder> AddGlobalForWasmAsync(this IBlazorComponentBuilder blazorComponentBuilder, string baseUri)
        {
            await blazorComponentBuilder.AddI18nForWasmAsync(Path.Combine(baseUri, $"i18n"));
            blazorComponentBuilder.Services.AddScoped<NavHelper>();
            blazorComponentBuilder.Services.AddScoped<GlobalConfig>();
            blazorComponentBuilder.Services.AddScoped<PermissionHelper>();

            return blazorComponentBuilder;
        }
    }
}

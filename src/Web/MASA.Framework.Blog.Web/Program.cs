using MASA.Framework.Blog.Web;
using MASA.Framework.Blog.Web.Global;
using MASA.Framework.Blog.Web.WebAssembly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();

await builder.Services.AddGlobalForWasmAsync(builder.HostEnvironment.BaseAddress);

var provider = builder.Services.BuildServiceProvider();
var globalConfig = provider.GetRequiredService<GlobalConfig>();
await globalConfig.Initialization();
var globalConfigsParamter = new Dictionary<string, object?> { [nameof(GlobalConfig)] = globalConfig };

builder.RootComponents.Add(typeof(App), "#app", ParameterView.FromDictionary(globalConfigsParamter));
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazor(builder =>
{
    builder.UseTheme(option =>
    {
        option.Primary = "#4318FF";
        option.Accent = "#4318FF";
    });
});

await builder.Build().RunAsync();

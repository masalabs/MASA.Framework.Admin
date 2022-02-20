using MASA.Framework.Admin.Blog.Filters;
using MASA.Framework.Admin.Caller;
using MASA.Framework.Development.Dapr;
using MASA.Utils.Caller.Core;
using MASA.Utils.Caller.HttpClient;
using MASA.Utils.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);

//if (builder.Environment.IsDevelopment())
//{
//    DaprStarter.Start(AppSettings.GetModel<DaprConfig>("DaprStarter"));
//}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaBlazor(builder =>
{
    builder.UseExceptionFilter<GlobalExceptionFilter>();
    builder.UseTheme(option =>
        {
            option.Primary = "#4318FF";
            option.Accent = "#4318FF";
        }
    );
});
builder.Services.AddMasaBlazorExperimentalComponents();

builder.Services.AddGlobalForServer();

builder.Services.AddCaller(typeof(BlogCaller).Assembly);
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //DaprStarter.Start(AppSettings.GetModel<DaprConfig>("DaprStarter"));
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseGlobal();
app.UseMasaI18n();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
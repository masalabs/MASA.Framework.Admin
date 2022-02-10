using MASA.Framework.Admin.Service.Authentication.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<PermissionService>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MASA.Framework.Admin - Authentications HTTP API",
            Version = "v1",
            Description = "The Authentications Service HTTP API"
        });
    }).AddServices(builder);

app.UseSwagger().UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA EShop Service HTTP API v1");
});
app.Run();

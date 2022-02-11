using MASA.Framework.Admin.Service.Configuration.Extensions.Middleware;
using MASA.Framework.Admin.Service.Configuration.Extensions.Middleware.GlobalException;
using MASA.Framework.Admin.Service.Configuration.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<MenuService>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MASA.Framework.Admin - Configurations HTTP API",
            Version = "v1",
            Description = "The Configurations Service HTTP API"
        });
    }).AddServices(builder);

app.UseGlobalExceptionMiddleware()
   .UseSwagger()
   .UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
   });

app.Run();

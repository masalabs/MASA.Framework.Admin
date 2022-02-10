using FluentValidation.AspNetCore;
using MASA.Framework.Admin.Service.User.Middleware;
using MASA.Framework.Admin.Service.User.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<UserServices>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MASA.Framework.Admin - Users HTTP API",
            Version = "v1",
            Description = "The Users Service HTTP API"
        });
    }).AddServices(builder);

app.UseSwagger().UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA EShop Service HTTP API v1");
});

app.Run();

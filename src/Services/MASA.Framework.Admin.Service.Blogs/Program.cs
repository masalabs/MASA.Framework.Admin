
using System.Reflection;
using FluentValidation.AspNetCore;
using MASA.Contrib.Data.UoW.EF;
using MASA.Contrib.DDD.Domain;
using MASA.Contrib.DDD.Domain.Repository.EF;
using MASA.Contrib.Dispatcher.Events;
using MASA.Contrib.Dispatcher.IntegrationEvents.Dapr;
using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using Microsoft.EntityFrameworkCore;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Infrastructure;
using MASA.Framework.Configuration;
using MASA.Framework.Development.Dapr;
using MASA.Utils.Caching.DistributedMemory.DependencyInjection;
using MASA.Utils.Caching.Redis.DependencyInjection;
using MASA.Utils.Caching.Redis.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//if (builder.Environment.IsDevelopment())
//{
//    DaprStarter.Start(AppSettings.GetModel<DaprConfig>("DaprStarter"));
//}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMasaRedisCache(AppSettings.GetModel<RedisConfigurationOptions>("Redis")).AddMasaMemoryCache();

builder.Services.AddDaprEventBus<IntegrationEventLogService>(options =>
{
    options.UseEventBus()
        .UseUoW<BlogDbContext>(dbOptions =>
            dbOptions.UseSqlServer(builder.Configuration["ConnectionString"]))
        .UseEventLog<BlogDbContext>();
});

//* minimal api зЂВс
var app = builder.Services.AddServices(builder);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

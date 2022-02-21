using MASA.Framework.Admin;
using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using MASA.Framework.Admin.Service.PageviewStatistics;
using MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<JobHostedService>();
builder.Services.AddDbContext<PageviewStatisticsDbContext>(options =>
{
    options.UseSqlServer("Server=.;Initial Catalog=Test;User ID=SA;Password=Pass@w0rd;");
});
builder.Services.AddScoped<IOperationLogRepository, OperationLogRepository>();
builder.Services.AddScoped<IPageviewDayStatisticsRepository, PageviewDayStatisticsRepository>();
builder.Services.AddScoped<IPageviewHourStatisticsRepository, PageviewHourStatisticsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSubscribeHandler();
});

app.Run();
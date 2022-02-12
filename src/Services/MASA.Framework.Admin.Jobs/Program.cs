using MASA.Framework.Admin;
using MASA.Framework.Admin.Repositories;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<JobHostedService>();
builder.Services.AddDbContext<AdminDbContext>(options =>
{
    options.UseSqlServer("Server=.;Initial Catalog=LogDB;User ID=SA;Password=Pass@w0rd;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();

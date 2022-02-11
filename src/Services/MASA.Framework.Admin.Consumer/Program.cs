using MASA.Framework.Admin.Consumer;
using MASA.Framework.Admin.Infrastructure;
using MASA.Framework.Admin.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RabbitMQEventBusOptions>(options =>
{
    options.HostName = "localhost";
    options.Port = 5673;
    options.UserName = "guest";
    options.Password = "guest";
    options.QueueName = "pubsub";
});
builder.Services.AddHostedService<RabbitMQConsumerHostedService>();

builder.Services.AddDbContext<AdminDbContext>(options =>
{
    options.UseSqlServer("Server=.;Initial Catalog=LogDB;User ID=SA;Password=P@ssw0rd;");
});
builder.Services.AddScoped<IOperationLogRepository, OperationLogRepository>();

var app = builder.Build();
app.Run();


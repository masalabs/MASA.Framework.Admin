using MASA.Framework.Admin.Api;
using MASA.Framework.Admin.Infrastructure;
using MASA.Framework.Admin.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AdminDbContext>(options =>
{
    options.UseSqlServer("Server=.;Initial Catalog=LogDB;User ID=SA;Password=P@ssw0rd;");
});
builder.Services.AddScoped<IOperationLogRepository, OperationLogRepository>();
builder.Services.AddRabbitMQEventBus(options =>
{
    options.HostName = "localhost";
    options.Port = 5673;
    options.UserName = "guest";
    options.Password = "guest";
    options.QueueName = "pubsub";
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

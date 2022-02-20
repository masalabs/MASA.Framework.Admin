using MASA.Framework.Admin.Repositories;
using MASA.Framework.Admin.Service.Logging.Infrastructure.Repositories;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MASA.Framework.Admin.Contracts.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LoggingDbContext>(options =>
{
    options.UseSqlServer("Server=.;Initial Catalog=Test;User ID=SA;Password=Pass@w0rd;");
});
builder.Services.AddScoped<IOperationLogRepository, OperationLogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSubscribeHandler();
});

app.Run();
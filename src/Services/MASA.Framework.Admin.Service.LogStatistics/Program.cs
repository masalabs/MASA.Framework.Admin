using Masa.Contrib.Configuration;
using Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Jobs;
using Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(AppConfigOption).Assembly);

builder.Services.AddLogging();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<JobHostedService>();

var app = builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<OperationLogService>();
})
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Masa.Framework.Admin Logs - Users HTTP API",
            Version = "v1",
            Description = "The Users Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<LogStatisticsDbContext>(dbOptions =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider()!;
                var option = serviceProvider
                    .GetRequiredService<IOptions<AppConfigOption>>();
                dbOptions.UseSqlServer(option.Value.DbConn);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<LogStatisticsDbContext>()
            .UseRepository<LogStatisticsDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<LogStatisticsDbContext>((context, services) =>
{

});

app.UseMasaExceptionHandling(opt =>
{
    opt.CustomExceptionHandler = exception =>
    {
        Exception friendlyException = exception;
        if (exception is ValidationException validationException)
        {
            friendlyException = new UserFriendlyException(validationException.Errors.Select(err => err.ToString()).FirstOrDefault()!);
        }
        return (friendlyException, false);
    };
})
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masa.Framework.Admin Service HTTP API v1");
    });


app.Run();

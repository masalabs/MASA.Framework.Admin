using Masa.Contrib.Dispatcher.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<JobHostedService>();

var app = builder.Services
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<OperationLogService>();
    })
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
    .AddDomainEventBus(dispatcherOption =>
    {
        dispatcherOption.UseIntegrationEventBus(option => option.UseDapr().UseEventLog<LogStatisticsDbContext>())
            .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
            .UseUoW<LogStatisticsDbContext>(dbOptions => dbOptions.UseFilter().UseSqlServer())
            .UseRepository<LogStatisticsDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<LogStatisticsDbContext>((context, services) =>
{

});

app.UseMasaExceptionHandler(option =>
    {
        option.ExceptionHandler = context =>
        {
            if (context.Exception is ValidationException validationException)
            {
                context.ToResult(validationException.Errors.Select(validationFailure => validationFailure.ToString()).FirstOrDefault()!);
            }
        };
    })
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masa.Framework.Admin Service HTTP API v1");
    });


app.Run();

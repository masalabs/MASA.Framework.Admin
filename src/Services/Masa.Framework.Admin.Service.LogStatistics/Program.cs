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
        dispatcherOption.UseDaprEventBus<IntegrationEventLogService>(option => option.UseEventLog<LogStatisticsDbContext>())
                        .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
                        .UseUoW<LogStatisticsDbContext>(dbOptions => dbOptions.UseFilter().UseSqlServer())
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

var builder = WebApplication.CreateBuilder(args);

var configOption = builder.Configuration.GetSection("AppConfig").Get<AppConfigOption>();

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
                dbOptions.UseSqlServer(configOption.DbConn);
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

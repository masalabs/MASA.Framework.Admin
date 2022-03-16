var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

builder.AddMasaConfiguration();

#if DEBUG

//builder.Services.AddDaprStarter();

#endif

var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<MenuService>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Masa.Framework.Admin - Configurations HTTP API",
            Version = "v1",
            Description = "The Configurations Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<ConfigurationDbContext>(dbOptions =>
            {
                dbOptions.UseSqlServer(builder.Configuration["Local:Appsettings:ConnectionStrings:DefaultConnection"]);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<ConfigurationDbContext>()
            .UseRepository<ConfigurationDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<ConfigurationDbContext>((context, services) =>
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

//app.Services.GetService

app.Run();

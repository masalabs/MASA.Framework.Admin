using Masa.Contrib.Dispatcher.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

#if DEBUG

//builder.Services.AddDaprStarter();

#endif

var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<MenuService>();
    })
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
    .AddDomainEventBus(dispatcherOption =>
    {
        dispatcherOption.UseIntegrationEventBus(option => option.UseDapr().UseEventLog<ConfigurationDbContext>())
            .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
            .UseUoW<ConfigurationDbContext>(dbOptions => dbOptions.UseFilter().UseSqlServer())
            .UseRepository<ConfigurationDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<ConfigurationDbContext>((context, services) =>
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

//app.Services.GetService

app.Run();

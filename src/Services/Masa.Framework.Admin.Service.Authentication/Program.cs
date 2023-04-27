using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG

//builder.Services.AddDaprStarter();

#endif

builder.Services.AddDistributedCache(options=>options.UseStackExchangeRedisCache());

var app = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Masa.Framework.Admin User - Authentications HTTP API",
            Version = "v1",
            Description = "The Authentications Service HTTP API"
        });
    })
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<AuthenticationDbContext>();
    })
    .AddMasaDbContext<AuthenticationDbContext>(dbOptions => dbOptions.UseSqlServer().UseFilter())
    .AddDomainEventBus(
        new[] { typeof(AuthenticationDbContext).Assembly, typeof(AddRolePermissionIntegraionEvent).Assembly },
        dispatcherOption =>
        {
            dispatcherOption.UseIntegrationEventBus(option => option.UseDapr().UseEventLog<AuthenticationDbContext>())
                .UseEventBus(eventBusBuilder => eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
                .UseUoW<AuthenticationDbContext>()
                .UseRepository<AuthenticationDbContext>();
        }
    )
    .AddServices(builder);

app.MigrateDbContext<AuthenticationDbContext>((context, services) =>
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
});

app.UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masa.Framework.Admin Service HTTP API v1");
    });

app.UseRouting();
app.UseCloudEvents();
app.UseEndpoints(endpoint =>
{
    endpoint.MapSubscribeHandler();
});

app.Run();

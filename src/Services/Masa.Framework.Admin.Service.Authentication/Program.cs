var builder = WebApplication.CreateBuilder(args);

#if DEBUG

//builder.Services.AddDaprStarter();

#endif

builder.Services
    .AddMasaRedisCache(builder.Configuration.GetSection("RedisConfig"))
    .AddMasaMemoryCache();

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
    .AddDomainEventBus(
        new[] { typeof(AuthenticationDbContext).Assembly, typeof(AddRolePermissionIntegraionEvent).Assembly },
        dispatcherOption =>
        {
            dispatcherOption.UseDaprEventBus<IntegrationEventLogService>(option => option.UseEventLog<AuthenticationDbContext>())
                .UseEventBus(eventBusBuilder => eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
                .UseUoW<AuthenticationDbContext>(dbOptions => dbOptions.UseFilter().UseSqlServer())
                .UseRepository<AuthenticationDbContext>();
        }
    )
    .AddServices(builder);

app.MigrateDbContext<AuthenticationDbContext>((context, services) =>
{
});

app.UseMasaExceptionHandling(options =>
{
    options.CustomExceptionHandler = exception =>
    {
        Exception friendlyException = exception;
        if (exception is ValidationException validationException)
        {
            friendlyException = new UserFriendlyException(validationException.Errors.Select(err => err.ToString()).FirstOrDefault()!);
        }
        return (friendlyException, false);
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

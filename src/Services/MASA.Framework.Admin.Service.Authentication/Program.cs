var builder = WebApplication
    .CreateBuilder(args)
    .AddMasaConfiguration(
        configurationBuilder =>
        {
            configurationBuilder.UseMasaOptions(options =>
            {
                options.Mapping<RedisConfigurationOptions>(SectionTypes.Local, "Appsettings",
                    "RedisConfig"); //Map the RedisConfigurationOptions binding to the Local:Appsettings:RedisConfig node
            });
        },
        assemblies: typeof(AppConfigOption).Assembly);

var serviceProvider = builder.Services.BuildServiceProvider()!;
var redisOptions = serviceProvider.GetService<IOptions<RedisConfigurationOptions>>();
builder.Services
    .AddMasaRedisCache(redisOptions!.Value)
    .AddMasaMemoryCache();

var appConfigOption = serviceProvider.GetRequiredService<IOptions<AppConfigOption>>();
if (appConfigOption.Value.EnableDapr)
    builder.Services.AddDaprStarter();

var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<AuthenticationDbContext>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
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
    .AddDomainEventBus(options =>
    {
        options.UseEventBus(typeof(AuthenticationDbContext).Assembly, typeof(AddRolePermissionIntegraionEvent).Assembly)
            .UseUoW<AuthenticationDbContext>(dbOptions =>
            {
                var option = serviceProvider
                    .GetRequiredService<IOptions<AppConfigOption>>();
                dbOptions.UseSqlServer(option.Value.DbConn);
                dbOptions.UseSoftDelete(builder.Services);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<AuthenticationDbContext>()
            .UseRepository<AuthenticationDbContext>();
    })
    .AddServices(builder);
app.MigrateDbContext<AuthenticationDbContext>((context, services) =>
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

app.UseRouting();
app.UseCloudEvents();
app.UseEndpoints(endpoint =>
{
    endpoint.MapSubscribeHandler();
});

app.Run();

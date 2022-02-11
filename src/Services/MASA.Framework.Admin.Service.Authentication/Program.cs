var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions).Assembly);

var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<PermissionService>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MASA.Framework.Admin User - Authentications HTTP API",
            Version = "v1",
            Description = "The Authentications Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<AuthenticationDbContext>(dbOptions =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider()!;
                var option = serviceProvider
                    .GetRequiredService<IOptions<MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions>>();
                dbOptions.UseSqlServer(option.Value.DbConn);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<AuthenticationDbContext>()
            .UseRepository<AuthenticationDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<AuthenticationDbContext>((context, services) =>
{
});
app.UseGlobalExceptionMiddleware()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
    });

app.Run();

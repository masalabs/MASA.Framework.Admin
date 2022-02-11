var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions).Assembly);
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
            Title = "MASA.Framework.Admin - Configurations HTTP API",
            Version = "v1",
            Description = "The Configurations Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<ConfigurationDbContext>(dbOptions =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider()!;
                var option = serviceProvider
                    .GetRequiredService<IOptions<MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions>>();
                dbOptions.UseSqlServer(option.Value.DbConn);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<ConfigurationDbContext>()
            .UseRepository<ConfigurationDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<ConfigurationDbContext>((context, services) =>
{
});
app.UseGlobalExceptionMiddleware()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
    });

app.Run();

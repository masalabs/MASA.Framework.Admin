using MASA.Contrib.Dispatcher.IntegrationEvents.Dapr.Processor;
using MASA.Utils.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions).Assembly);

builder.Services.AddLogging();
var app = builder.Services.AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<UserServices>();
    })
    .AddTransient(typeof(IMiddleware<>), typeof(ValidatorMiddleware<>))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MASA.Framework.Admin User - Users HTTP API",
            Version = "v1",
            Description = "The Users Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<UserDbContext>(dbOptions =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider()!;
                var option = serviceProvider
                    .GetRequiredService<IOptions<MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions>>();
                dbOptions.UseSqlServer(option.Value.DbConn);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<UserDbContext>()
            .UseRepository<UserDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<UserDbContext>((context, services) =>
{
});
app.UseMasaExceptionHandling()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
    });

app.Run();

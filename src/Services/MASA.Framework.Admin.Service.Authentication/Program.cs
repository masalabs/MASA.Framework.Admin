using MASA.Contrib.Data.Contracts.EF;
using MASA.Utils.Exceptions.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMasaConfiguration(
        null,
        assemblies: typeof(DbConnectionOption).Assembly);
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
                    .GetRequiredService<IOptions<DbConnectionOption>>();
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
app.UseMasaExceptionHandling()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
    });

app.Run();

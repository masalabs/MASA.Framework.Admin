var builder = WebApplication.CreateBuilder(args);
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
            Title = "MASA.Framework.Admin - Users HTTP API",
            Version = "v1",
            Description = "The Users Service HTTP API"
        });
    })
    .AddDomainEventBus(options =>
    {
        options.UseEventBus()
            .UseUoW<UserDbContext>(dbOptions => dbOptions.UseSqlServer("server=masa.admin.database;uid=sa;pwd=P@ssw0rd;database=blog"))
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<UserDbContext>()
            .UseRepository<UserDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<UserDbContext>((context, services) =>
{
});

app.UseGlobalExceptionMiddleware()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MASA.Framework.Admin Service HTTP API v1");
    });

app.Run();

var builder = WebApplication.CreateBuilder(args);
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
            .UseUoW<AuthenticationDbContext>(dbOptions => dbOptions.UseSqlServer("server=masa.admin.database;uid=sa;pwd=P@ssw0rd;database=blog_authications"))
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

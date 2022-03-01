using Masa.Framework.Admin.Service.User.Domain.Services;
using Masa.Framework.Admin.Service.User.Infrastructure.Hub;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(AppConfigOption).Assembly);

builder.Services.AddScoped<LoginService>();
builder.Services.AddSignalR().AddHubOptions<LoginHub>(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddLogging();

builder.Services.AddMemoryCache();

builder.Services.AddOpenTelemetryTracing(options =>
    options
    .AddSource(TelemetryConstants.ServiceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(serviceName: TelemetryConstants.ServiceName, serviceVersion: TelemetryConstants.ServiceVersion).AddTelemetrySdk())
        .AddSqlClientInstrumentation(options =>
        {
            options.SetDbStatementForText = true;
            options.RecordException = true;
        })
        .AddAspNetCoreInstrumentation(options =>
        {
            options.Filter = (req) => !req.Request.Path.ToUriComponent().Contains("index.html", StringComparison.OrdinalIgnoreCase)
                && !req.Request.Path.ToUriComponent().Contains("swagger", StringComparison.OrdinalIgnoreCase);
        })
        .AddHttpClientInstrumentation()
        .AddZipkinExporter(o =>
        {
            o.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
        })
    //.AddOtlpExporter(otlpOptions =>
    //{
    //    otlpOptions.Endpoint = new Uri(builder.Configuration.GetValue<string>("AppSettings:OtelEndpoint"));
    //})
    );

//https://www.meziantou.net/monitoring-a-dotnet-application-using-opentelemetry.htm
//TODO change zipkin to OTLP

//builder.Services.AddOpenTelemetryMetrics(options =>
//    options.AddHttpClientInstrumentation()
//     .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: TelemetryConstants.ServiceName, serviceVersion: TelemetryConstants.ServiceVersion).AddTelemetrySdk())
//     .AddMeter(meterName)
//);

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
            Title = "Masa.Framework.Admin User - Users HTTP API",
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
                    .GetRequiredService<IOptions<AppConfigOption>>();
                dbOptions
                    .UseSqlServer(option.Value.DbConn);
            })
            .UseDaprEventBus<IntegrationEventLogService>()
            .UseEventLog<UserDbContext>()
            .UseRepository<UserDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<UserDbContext>((context, services) =>
{
    if (context.Set<Masa.Framework.Admin.Service.User.Domain.Aggregates.User>().Any())
    {
        return;
    }
    context.Set<Masa.Framework.Admin.Service.User.Domain.Aggregates.User>().Add(new Masa.Framework.Admin.Service.User.Domain.Aggregates.User(Guid.Empty, "admin", "admin123",true)
    {
        Name = "Administrator",
        Email = "admin@masastack.com"
    });
    context.SaveChanges();
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
app.MapHub<LoginHub>("/hub/login",
                    options => options.Transports =
                        HttpTransportType.WebSockets |
                        HttpTransportType.LongPolling);

//app.UseEndpoints(endpoint =>
//{
//    endpoint.MapHub<LoginHub>("/hub/login",
//                    options => options.Transports =
//                        HttpTransportType.WebSockets |
//                        HttpTransportType.LongPolling);
//});

app.Run();

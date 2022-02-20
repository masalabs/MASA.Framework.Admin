
using MASA.Framework.Admin.Contracts.User.Const;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.AddMasaConfiguration(
    null,
    assemblies: typeof(MASA.Framework.Admin.Contracts.Base.Extensions.Configurations.DbContextOptions).Assembly);

builder.Services.AddLogging();

builder.Services.AddOpenTelemetryTracing(options =>
    options
    .AddSource(TelemetryConstants.ServiceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: TelemetryConstants.ServiceName, serviceVersion: TelemetryConstants.ServiceVersion).AddTelemetrySdk())
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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<LoginService>();
builder.Services.AddSignalR().AddHubOptions<LoginHub>(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddLogging();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(async options =>
{
    var SECRET_STORE_NAME = "localsecretstore";
    using var client = new DaprClientBuilder().Build();
    var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "jwt_security");
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret["jwt_security"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hub/login")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddMemoryCache();

builder.Services.AddOpenTelemetryTracing(options =>
        options
            .AddSource(TelemetryConstants.SERVICE_NAME)
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService(serviceName: TelemetryConstants.SERVICE_NAME, serviceVersion: TelemetryConstants.SERVICE_VERSION)
                .AddTelemetrySdk())
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
    .AddDomainEventBus(dispatcherOption =>
    {
        dispatcherOption.UseIntegrationEventBus(option => option.UseDapr().UseEventLog<UserDbContext>())
            .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
            .UseUoW<UserDbContext>(dbOptions => dbOptions.UseFilter().UseSqlServer())
            .UseRepository<UserDbContext>();
    })
    .AddServices(builder);

app.MigrateDbContext<UserDbContext>((context, services) =>
{
    if (context.Set<Masa.Framework.Admin.Service.User.Domain.Aggregates.User>().Any())
    {
        return;
    }

    context.Set<Masa.Framework.Admin.Service.User.Domain.Aggregates.User>().Add(
        new Masa.Framework.Admin.Service.User.Domain.Aggregates.User(Guid.Empty, "admin", "admin123", true)
        {
            Name = "Administrator",
            Email = "admin@masastack.com"
        });
    context.SaveChanges();
});

app.UseMasaExceptionHandler(option =>
    {
        option.ExceptionHandler = context =>
        {
            if (context.Exception is ValidationException validationException)
            {
                context.ToResult(validationException.Errors.Select(validationFailure => validationFailure.ToString()).FirstOrDefault()!);
            }
        };
    })
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masa.Framework.Admin Service HTTP API v1");
    });

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();
app.UseEndpoints(endpoint =>
{
    endpoint.MapSubscribeHandler();
    app.MapHub<LoginHub>("/hub/login",
        options => options.Transports =
            HttpTransportType.WebSockets |
            HttpTransportType.LongPolling);
});

app.Run();

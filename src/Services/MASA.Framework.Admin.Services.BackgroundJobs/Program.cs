var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.RegisterBackgroundJobWorker();
builder.Services.AddHostedService<BackgroundJobService>();

builder.Services.AddTransient(typeof(IMiddleware<>), typeof(LogMiddleware<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });
builder.Services.AddDaprEventBus<IntegrationEventLogService>(options =>
{
    options.UseEventBus()
           .UseUoW<JobDbContext>(dbOptions =>
            dbOptions.UseSqlServer("Server=10.10.90.67,30140,30060;DataBase=backgroundjob;uid=sa;pwd=p@ssw0rd"))
           .UseEventLog<JobDbContext>();
});

builder.Services.AddEventBus();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IBackgroundJobStore, JobRepository>();
builder.Services.AddScoped<IQueryRepository, JobRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

MigrateDbContext<JobDbContext>(app, (context, services) =>
{
    context.SaveChanges();
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});

app.MapControllers();

app.Run();


void MigrateDbContext<TContext>(IHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
{
    using (var scope = webHost.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TContext>();
        context.Database.Migrate();
        seeder(context, services);
    }
}
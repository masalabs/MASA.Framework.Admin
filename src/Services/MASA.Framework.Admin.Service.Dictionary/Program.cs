
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddControllers();

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
           .UseUoW<DictionaryDbContext>(dbOptions => 
           dbOptions.UseSqlServer("Server=10.10.90.67,30160,30060;DataBase=PND;uid=sa;pwd=p@ssw0rd"))
           .UseEventLog<DictionaryDbContext>();
});

builder.Services.AddMasaRedisCache(AppSettings.GetModel<RedisConfigurationOptions>("Redis")).AddMasaMemoryCache();

builder.Services.AddEventBus();
builder.Services.AddScoped<IDicRepository, DicRepository>();
builder.Services.AddScoped<IDicValueRepository, DicValueRepository>();

var app = builder.Services.AddServices(builder);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDbContext<DictionaryDbContext>((context, services) =>
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

using MASA.Contrib.Data.Contracts.EF;
using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Framework.Admin.Service.Login.Hub;
using MASA.Framework.Admin.Service.Login.Infrastructure.Utils;
using MASA.Framework.Admin.Service.Order.Infrastructure.Filters;
using MASA.Utils.Configuration.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddControllers(
    options =>
    {
        //options.Filters.Add<ApiAuthorizeFilter>();
    });

var repositories = Assembly.GetExecutingAssembly();
var assembly = repositories
    .DefinedTypes
    .Where(a => a.Name.EndsWith("Repository") && !a.Name.StartsWith("I"));
foreach (var item in assembly)
{
    builder.Services.AddScoped(item.GetInterfaces().First(), item);
}


var connectionString = AppSettings.Get("ConnectionString");
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
           .UseUoW<MyDbContext>(dbOptions =>
           {
               dbOptions.UseSqlServer(connectionString);
               dbOptions.UseSoftDelete(builder.Services);//Start soft delete
           })
           .UseEventLog<MyDbContext>();
});

AuthOptions _authOptions = AppSettings.GetModel<AuthOptions>("AuthOptions");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Security)),
        ValidateIssuer = false,
        ValidateAudience = false,
        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        // 将clockskew设置为0，使令牌恰好在令牌到期时间到期(而不是5分钟后)
        ClockSkew = TimeSpan.Zero
    };

    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/login")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddSignalR();



builder.Services.AddEventBus();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();
app.MapHub<LoginHub>("/login");
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});

app.MapControllers();

app.Run();

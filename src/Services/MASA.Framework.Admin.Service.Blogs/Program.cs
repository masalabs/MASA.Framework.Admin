using MASA.Contrib.Data.Contracts.EF;
using MASA.Contrib.Data.UoW.EF;
using MASA.Contrib.Dispatcher.IntegrationEvents.Dapr;
using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Infrastructure;
using MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys;
using MASA.Framework.Development.Dapr;
using MASA.Utils.Caching.DistributedMemory.DependencyInjection;
using MASA.Utils.Caching.Redis.DependencyInjection;
using MASA.Utils.Caching.Redis.Models;
using MASA.Utils.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);

//if (builder.Environment.IsDevelopment())
//{
//    DaprStarter.Start(AppSettings.GetModel<DaprConfig>("DaprStarter"));
//}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMasaRedisCache(AppSettings.GetModel<RedisConfigurationOptions>("Redis")).AddMasaMemoryCache();
builder.Services.AddScoped<IBlogArticleRepository, BlogArticleRepository>();

builder.Services.AddDaprEventBus<IntegrationEventLogService>(options =>
{
    options.UseEventBus()
        .UseUoW<BlogDbContext>(dbOptions =>
        {
            dbOptions.UseSqlServer(builder.Configuration["ConnectionString"]);
            dbOptions.UseSoftDelete(builder.Services);
        })
        .UseEventLog<BlogDbContext>();
});

builder.Services.AddScoped<IBlogAdvertisingPicturesRepository, BlogAdvertisingPicturesRepository>();
builder.Services.AddScoped<IBlogArticleRepository, BlogArticleRepository>();
builder.Services.AddScoped<IBlogCommentInfoRepository, BlogCommentInfoRepository>();
builder.Services.AddScoped<IBlogEnclosureInfoRepository, BlogEnclosureInfoRepository>();
builder.Services.AddScoped<IBlogLabelRepository, BlogLabelRepository>();
builder.Services.AddScoped<IBlogTypeRepository, BlogTypeRepository>();
builder.Services.AddScoped<IBlogApprovedRecordRepository, BlogApprovedRecordRepository>();
builder.Services.AddScoped<IBlogReportRepository, BlogReportRepository>();
builder.Services.AddScoped<IElasticClientProvider, ElasticClientProvider>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<BlogAppSettiings>(builder.Configuration);

//* minimal api ע��
var app = builder.Services.AddServices(builder);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var daprConfig = AppSettings.GetModel<DaprConfig>("DaprStarter");
    DaprStarter.Start(daprConfig);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

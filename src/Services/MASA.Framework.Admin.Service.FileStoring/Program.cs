using MASA.Framework.Admin.Infrastructures.FileStoring;
using MASA.Framework.Admin.Infrastructures.FileStoring.Minio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddMinioFileStoring();
builder.Services.Configure<FileStoringOptions>(options =>
{
    options.Container.UseMinio(minio =>
    {
        minio.EndPoint = builder.Configuration["MinIO:EndPoint"];
        minio.AccessKey = builder.Configuration["MinIO:AccessKey"];
        minio.SecretKey = builder.Configuration["MinIO:SecretKey"];
        minio.BucketName = builder.Configuration["MinIO:Bucket"];
        minio.WithSSL = Convert.ToBoolean(builder.Configuration["MinIO:WithSSL"]);
        minio.CreateBucketIfNotExists = Convert.ToBoolean(builder.Configuration["MinIO:CreateBucketIfNotExists"]);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using MASA.Framework.Admin.Caller.Blogs;
using MASA.Framework.Dapr.DaprClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MASA.Framework.Admin.Caller;

public class BlogCaller : HttpClientCallerBase
{
    //private ArticleService _articleService;

    //protected override string BaseAddress { get; set; } = "http://localhost:18500";

    //public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    //{
    //    Name = nameof(BlogCaller);
    //}

    //public ArticleService ArticleService => _articleService ?? new ArticleService(CallerProvider);


    //private const string APP_ID = "masa-framework-admin-service-blogs";

    //public IMasaHttpClient HttpClient { get; init; }

    //public BlogCaller(IWebHostEnvironment env, ILogger<MasaHttpDaprClient> logger)
    //{
    //    HttpClient = new MasaHttpDaprClient(APP_ID, logger);
    //    ArticleService = new ArticleService(HttpClient);
    //}

    //public ArticleService ArticleService { get; init; }
    public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Name = nameof(BlogCaller);
    }

    public async Task GetBrandsAsync()
    {
        var response = await CallerProvider.PostAsync("/api/blogtype/create", new CreateBlogTypeModel()
        {
            TypeName = "測試 call"
        });
    }

    protected override string BaseAddress { get; set; } = "http://masa.admin.services.blogs";
}
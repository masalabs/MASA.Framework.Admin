using MASA.Framework.Admin.Caller.Blogs;
using MASA.Framework.Dapr.DaprClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MASA.Framework.Admin.Caller;

public class BlogCaller : HttpClientCallerBase
{
    private ArticleService _articleService;
    private BlogTypeService _blogTypeService;

    protected override string BaseAddress { get; set; } = "http://masa.admin.services.blogs";

    public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Name = nameof(BlogCaller);
    }

    public ArticleService ArticleService => _articleService ?? new ArticleService(CallerProvider);
    public BlogTypeService BlogTypeService => _blogTypeService ?? new BlogTypeService(CallerProvider);

    

}
using MASA.Framework.Admin.Caller.Blogs;

namespace MASA.Framework.Admin.Caller;

public class BlogCaller : HttpClientCallerBase
{
    private ArticleService _articleService;

    protected override string BaseAddress { get; set; } = "http://localhost:18500";

    public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Name = nameof(BlogCaller);
    }

    public ArticleService ArticleService => _articleService ?? new ArticleService(CallerProvider);
}
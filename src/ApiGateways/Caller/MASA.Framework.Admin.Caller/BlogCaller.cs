using MASA.Framework.Admin.Caller.Blogs;

namespace MASA.Framework.Admin.Caller;

public class BlogCaller : HttpClientCallerBase
{
    private ArticleService _articleService;
    private ReportService _reportService;
    private BlogTypeService _typeService;
    private AdvertisingPicturesService _advertisingPicturesService;
    private CommentsService _commentsService;

    protected override string BaseAddress { get; set; } = "http://masa.admin.services.blogs";

    public BlogCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Name = nameof(BlogCaller);
    }

    public ArticleService ArticleService => _articleService ?? new ArticleService(CallerProvider);
    public BlogTypeService BlogTypeService => _typeService ?? new BlogTypeService(CallerProvider);
    public AdvertisingPicturesService AdvertisingPicturesService => _advertisingPicturesService ?? new AdvertisingPicturesService(CallerProvider);
    public ReportService ReportService => _reportService ?? new ReportService(CallerProvider);
    public CommentsService CommentsService => _commentsService?? new CommentsService(CallerProvider);
}
namespace MASA.Framework.Admin.Caller.Blogs;

public class ArticleService
{
    private readonly ICallerProvider _callerProviderProvider;

    public ArticleService(ICallerProvider callerProvider)
    {
        _callerProviderProvider = callerProvider;
    }

    public Task<PagingResult<BlogInfoListViewModel>> GetList(GetBlogArticleOptions options)
    {
        return _callerProviderProvider.PostAsync<GetBlogArticleOptions, PagingResult<BlogInfoListViewModel>>(
            "/api/articles/GetList", options);
    }
}
namespace MASA.Framework.Admin.Caller.Blogs;

public class ArticleService
{

    private readonly ICallerProvider _callerProviderProvider;

    public ArticleService(ICallerProvider callerProvider)
    {
        _callerProviderProvider = callerProvider;
    }

    public async Task<PagingResult<BlogInfoListViewModel>> GetList(GetBlogArticleOptions options)
    {
        return await _callerProviderProvider.PostAsync<GetBlogArticleOptions, PagingResult<BlogInfoListViewModel>>(
            "/api/articles/GetList", options);
    }

    /// <summary>
    /// 博客首页列表
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<PagingResult<BlogInfoHomeListViewModel>> BlogArticleHomeAsync(GetBlogArticleHomeOptions options)
    {
        return await _callerProviderProvider.PostAsync<GetBlogArticleHomeOptions,
            PagingResult<BlogInfoHomeListViewModel>>("/​api/articles/BlogArticleHome", options);
    }

    /// <summary>
    /// 博客用户博文列表
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<PagingResult<BlogInfoListViewModel>> BlogArticleByUserAsync(GetBlogArticleUserOptions options)
    {
        return await _callerProviderProvider.PostAsync<GetBlogArticleUserOptions,
            PagingResult<BlogInfoListViewModel>>("/​api/articles/BlogArticleByUser", options);
    }
}
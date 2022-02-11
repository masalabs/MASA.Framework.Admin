using MASA.Framework.Dapr.DaprClient;

namespace MASA.Framework.Admin.Caller.Blogs;

public class ArticleService
{
    //private readonly ICallerProvider _callerProviderProvider;

    //public ArticleService(ICallerProvider callerProvider)
    //{
    //    _callerProviderProvider = callerProvider;
    //}

    //public Task<PagingResult<BlogInfoListViewModel>> GetList(GetBlogArticleOptions options)
    //{
    //    return _callerProviderProvider.PostAsync<GetBlogArticleOptions, PagingResult<BlogInfoListViewModel>>(
    //        "/api/articles/GetList", options);
    //}

    private const string BASE_API = "/api/articles";

    private readonly IMasaHttpClient _client;

    public ArticleService(IMasaHttpClient client) => _client = client;

    public Task<PagingResult<BlogInfoListViewModel>> GetList(GetBlogArticleOptions options)
    {
        return _client.PostAsync<GetBlogArticleOptions, PagingResult<BlogInfoListViewModel>>(
                $"{BASE_API}/GetList", options);
    }
}
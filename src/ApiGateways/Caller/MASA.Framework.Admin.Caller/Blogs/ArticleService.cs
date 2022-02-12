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
            PagingResult<BlogInfoHomeListViewModel>>("/api/articles/BlogArticleHome", options);
    }

    /// <summary>
    /// 博客用户博文列表
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<PagingResult<BlogInfoListViewModel>> BlogArticleByUserAsync(GetBlogArticleUserOptions options)
    {
        return await _callerProviderProvider.PostAsync<GetBlogArticleUserOptions,
            PagingResult<BlogInfoListViewModel>>("/api/articles/BlogArticleByUser", options);
    }

    /// <summary>
    /// 发布博客
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task CreateAsync(CreateBlogInfoModel request)
    {
        await _callerProviderProvider.PostAsync("/api/articles/Create", request);
    }

    /// <summary>
    /// 修改博客
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task UpdateAsync(UpdateBlogInfoModel request)
    {
        await _callerProviderProvider.PutAsync("/api/articles/Update", request);
    }

    /// <summary>
    /// 批量删除博客
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task RemoveAsync(Guid[] ids)
    {
        await _callerProviderProvider.DeleteAsync("/api/articles/Remove", ids);
    }

    /// <summary>
    /// 添加浏览记录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task AddVisitsAsync(AddBlogVisitModel request)
    {
        await _callerProviderProvider.PostAsync("/api/articles/AddVisits", request);
    }

    /// <summary>
    /// 点赞、取消点赞记录
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task AddBlogApprovedRecordAsync(BlogApprovedRecordModel request)
    {
        await _callerProviderProvider.PostAsync("/api/articles/AddBlogApprovedRecord", request);
    }

    /// <summary>
    /// 博客详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BlogInfoListViewModel> GetAsync(Guid id)
    {
        return await _callerProviderProvider.GetAsync<BlogInfoListViewModel>($"/api/articles/Get?id={id}");
    }

    /// <summary>
    /// 下架博客文章
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task WithdrawAsync(WithdrawBlogArticleModel model)
    {
        return _callerProviderProvider.PostAsync($"/api/articles/withdraw", model);
    }
}
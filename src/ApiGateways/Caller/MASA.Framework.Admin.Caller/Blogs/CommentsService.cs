namespace MASA.Framework.Admin.Caller.Blogs;

public class CommentsService
{
    private readonly ICallerProvider _callerProviderProvider;

    public CommentsService(ICallerProvider callerProvider)
    {
        _callerProviderProvider = callerProvider;
    }

    /// <summary>
    /// 新增评论
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task CreateAsync(AddCommentModel request)
    {
        await _callerProviderProvider.PostAsync("/api/blogComment/Create", request);
    }

    /// <summary>
    /// 删除评论
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task RemoveAsync(RemoveCommentModel request)
    {
        await _callerProviderProvider.PostAsync("/api/blogComment/Remove", request);
    }

    /// <summary>
    /// 博文评论列表取得
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<PagingResult<BlogCommentInfoListViewModel>> GetListAsync(
        GetBlogCommentInfoOptions options)
    {
        return await _callerProviderProvider.PostAsync<GetBlogCommentInfoOptions,
            PagingResult<BlogCommentInfoListViewModel>>("/api/blogComment/GetList", options);
    }
}
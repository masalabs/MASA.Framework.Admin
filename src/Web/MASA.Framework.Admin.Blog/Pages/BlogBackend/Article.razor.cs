namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class Article : ProCompontentBase
{
    [Inject]
    public BlogCaller BlogCaller { get; set; }

    #region table

    private GetBlogArticleOptions _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogInfoListViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogInfoListViewModel>> _headers = new()
    {
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "标题", Value = nameof(BlogInfoListViewModel.title), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "状态", Value = nameof(BlogInfoListViewModel.state), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "分类", Value = nameof(BlogInfoListViewModel.typeName), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "阅读量", Value = nameof(BlogInfoListViewModel.visits), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "评论数量", Value = nameof(BlogInfoListViewModel.commentCount), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "点赞数量", Value = nameof(BlogInfoListViewModel.approvedCount), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "发布时间", Value = nameof(BlogInfoListViewModel.ReleaseTime), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>() { Text = "操作", Value = "actions", Sortable = false }
    };

    #endregion

    private List<BlogTypeCondensedViewModel> BlogTypes = new();

    private bool _withdrawModalVisible;

    private BlogInfoListViewModel CurrentModel { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await FetchTypes();

            await FetchList();

            StateHasChanged();
        }
    }

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        await FetchList(options.Page, options.ItemsPerPage);
    }

    private async Task FetchList(int pageIndex = 1, int pageSize = 10)
    {
        _options.PageIndex = pageIndex;
        _options.PageSize = pageSize;

        _loading = true;

        var result = await BlogCaller.ArticleService.GetList(_options);
        _tableData = result.Data;
        _totalCount = result.TotalCount;

        _loading = false;
    }

    private async Task FetchTypes()
    {
        BlogTypes = await BlogCaller.BlogTypeService.GetAllAsync();
    }

    private void ShowWithdrawModal(BlogInfoListViewModel model)
    {
        CurrentModel = model;
        _withdrawModalVisible = true;
    }

    private async Task OnWithdraw(WithdrawBlogArticleModel model)
    {
        model.Id = CurrentModel.id;

        await BlogCaller.ArticleService.WithdrawAsync(model);
    }
}
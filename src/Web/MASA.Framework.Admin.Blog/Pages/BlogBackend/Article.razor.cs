using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Data.Mapping;

namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class Article : ProCompontentBase
{
    [Inject]
    public BlogCaller BlogCaller { get; set; }

    [Inject]
    protected NavigationManager? Navigation { get; set; }

    private GetBlogArticleOptions _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogInfoListViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogInfoListViewModel>> _headers = new()
    {
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "标题", Value = nameof(BlogInfoListViewModel.Title), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "状态", Value = nameof(BlogInfoListViewModel.State), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "分类", Value = nameof(BlogInfoListViewModel.TypeName), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "阅读量", Value = nameof(BlogInfoListViewModel.Visits), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "评论数量", Value = nameof(BlogInfoListViewModel.CommentCount), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "点赞数量", Value = nameof(BlogInfoListViewModel.ApprovedCount), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "发布时间", Value = nameof(BlogInfoListViewModel.ReleaseTime), Sortable = false },
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "操作", Value = "actions", Sortable = false }
    };

    private List<BlogTypeCondensedViewModel> BlogTypes = new();

    private bool _withdrawModalVisible;

    private BlogInfoListViewModel CurrentModel { get; set; }

    private UpdateBlogInfoModel _updateBlogInfoModel = new();

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

    /// <summary>
    /// 审核/上架
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task AuditArticleAsync(BlogInfoListViewModel model)
    {
        _updateBlogInfoModel =
              new Mapping<BlogInfoListViewModel, UpdateBlogInfoModel>().Map(model);

        _updateBlogInfoModel.State = StateTypes.Reviewed;

        await BlogCaller.ArticleService.UpdateAsync(_updateBlogInfoModel);

        Message("审核成功", AlertTypes.Success);

        await FetchList();
        StateHasChanged();
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
        model.Id = CurrentModel.Id;

        await BlogCaller.ArticleService.WithdrawAsync(model);

        _withdrawModalVisible = false;

        await FetchList(_options.PageIndex, _options.PageSize);
    }

    public void HrefDetailPage(Guid id)
    {
        Navigation?.NavigateTo($"/blog-admin/articledetail/{id}");
    }
}
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
        new DataTableHeader<BlogInfoListViewModel>()
        { Text = "操作", Value = "actions", Width = 200, Sortable = false }
    };

    private List<BlogTypeCondensedViewModel> _blogTypes = new();

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

    /// <summary>
    /// 下架
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task TakeDownArticleAsync(BlogInfoListViewModel model)
    {
        _updateBlogInfoModel =
            new Mapping<BlogInfoListViewModel, UpdateBlogInfoModel>().Map(model);

        _updateBlogInfoModel.State = StateTypes.OffTheShelf;

        await BlogCaller.ArticleService.UpdateAsync(_updateBlogInfoModel);

        Message("下架成功", AlertTypes.Success);

        await FetchList();
        StateHasChanged();
    }

    private async Task FetchTypes()
    {
        _blogTypes = await BlogCaller.BlogTypeService.GetAllAsync();
    }

    public void HrefDetailPage(Guid id)
    {
        Navigation?.NavigateTo($"/blog-admin/articledetail/{id}");
    }
}
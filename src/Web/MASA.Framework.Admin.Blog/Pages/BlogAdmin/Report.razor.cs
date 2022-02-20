using MASA.Framework.Admin.Contracts.Blogs.BlogReport.Options;
using MASA.Framework.Admin.Contracts.Blogs.BlogReport.ViewModel;

namespace MASA.Framework.Admin.Blog.Pages.BlogAdmin;

public partial class Report : ProCompontentBase
{
    [Inject]
    public BlogCaller BlogCaller { get; set; }

    #region table

    private GetBlogReportOptions _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogReportListViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogReportListViewModel>> _headers = new()
    {
        new DataTableHeader<BlogReportListViewModel>()
            { Text = "举报标题", Value = nameof(BlogReportListViewModel.Title), Sortable = false },
        new DataTableHeader<BlogReportListViewModel>()
            { Text = "举报理由", Value = nameof(BlogReportListViewModel.Reason), Sortable = false },
        new DataTableHeader<BlogReportListViewModel>()
            { Text = "举报详细信息", Value = nameof(BlogReportListViewModel.Detail), Sortable = false },
        new DataTableHeader<BlogReportListViewModel>()
            { Text = "举报时间", Value = nameof(BlogReportListViewModel.CreationTime), Sortable = false },
        new DataTableHeader<BlogReportListViewModel>()
            { Text = "操作", Value = nameof(BlogReportListViewModel.Handled), Sortable = false },
    };

    #endregion

    private bool _handleModalVisible;
    private BlogReportListViewModel CurrentModel { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
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

        StateHasChanged();

        var result = await BlogCaller.ReportService.GetList(_options);
        _tableData = result.Data;
        _totalCount = result.TotalCount;

        _loading = false;
    }

    private void OpenHandleModal(BlogReportListViewModel model)
    {
        CurrentModel = model;
        _handleModalVisible = true;
    }

    private async Task HandleIgnore(Guid reportId)
    {
        await BlogCaller.ReportService.IgnoreAsync(new IgnoreBlogReportModel() { Id = reportId });

        _handleModalVisible = false;
        
        await FetchList(_options.PageIndex, _options.PageSize);
    }

    private async Task HandleAgree(Guid articleId)
    {
        await BlogCaller.ReportService.AgreeAsync(new AgreeBlogReportModel() { ArticleId = articleId });

        _handleModalVisible = false;

        await FetchList(_options.PageIndex, _options.PageSize);
    }
}
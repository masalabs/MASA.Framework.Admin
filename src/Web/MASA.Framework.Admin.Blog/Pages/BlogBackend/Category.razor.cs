namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class Category : ProCompontentBase
{
    private GetBlogTypePagingOption _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogTypeListViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogTypeListViewModel>> _headers = new()
    {
        new()
            {Text = "名称", Value = nameof(BlogTypeListViewModel.TypeName), Sortable = false},
        new()
            {Text = "创建时间", Value = nameof(BlogTypeListViewModel.CreationTime), Sortable = false},
        new()
            {Text = "操作", Value = "actions", Sortable = false}
    };

    private async Task FetchList(int pageIndex = 1, int pageSize = 10)
    {
        _options.PageIndex = pageIndex;
        _options.PageSize = pageSize;

        _loading = true;

        // TODO: http

        _loading = false;
    }

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        await FetchList(options.Page, options.ItemsPerPage);
    }
}
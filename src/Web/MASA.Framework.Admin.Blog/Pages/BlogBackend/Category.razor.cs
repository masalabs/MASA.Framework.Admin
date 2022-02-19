using MASA.Framework.Admin.Caller;
using MASA.Framework.Data.Mapping;

namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class Category : ProCompontentBase
{
    private GetBlogTypePagingOption _options = new();
    private int _totalCount = 0;
    private bool _loading = false;
    private List<BlogTypePagingViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogTypePagingViewModel>> _headers = new()
    {
        new()
        { Text = "名称", Value = nameof(BlogTypePagingViewModel.TypeName), Sortable = false },
        new()
        { Text = "创建时间", Value = nameof(BlogTypePagingViewModel.CreationTime), Sortable = false },
        new()
        { Text = "操作", Value = "actions", Sortable = false }
    };

    private DataModal<BlogTypePagingViewModel> _dataModal = new();
    private CreateBlogTypeModel _createBlogTypeModel = new();
    private UpdateBlogTypeModel _updateBlogTypeModel = new();
    private string _dialogTitle = string.Empty;
    [Inject] protected BlogCaller BlogCaller { get; set; }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await FetchList();

            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task FetchList(int pageIndex = 1, int pageSize = 10)
    {
        _options.PageIndex = pageIndex;
        _options.PageSize = pageSize;

        _loading = true;

        var result = await BlogCaller.BlogTypeService.PagingAsync(_options);

        _tableData = result.Data;
        _totalCount = result.TotalCount;

        _loading = false;
    }

    /// <summary>
    /// 新增
    /// </summary>
    private async Task AddAsync()
    {
        _dataModal.Visible = true;
        _dialogTitle = "新增";
    }

    public async Task UpdateAsync(BlogTypePagingViewModel model)
    {
        _dialogTitle = "更新";
        _dataModal.Visible = true;
        _dataModal.Show(model);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    public async Task DelAsync(BlogTypePagingViewModel model)
    {
        Confirm(
               title: "删除文章类型",
               content: $"您确认要文章类型（{model.TypeName}）吗？",
               onOk: async () =>
               {
                   Guid[] ids = { model.Id };
                   await BlogCaller.BlogTypeService.RemoveAsync(ids);

                   Message("删除成功", AlertTypes.Success);

               }, AlertTypes.Warning);

        StateHasChanged();
    }

    private async Task Save()
    {
        var result = Guid.Empty;

        if (!_dataModal.HasValue)
        {
            _createBlogTypeModel =
                new Mapping<BlogTypePagingViewModel, CreateBlogTypeModel>().Map(_dataModal.Data);

            await BlogCaller.BlogTypeService.CreateAsync(_createBlogTypeModel);

            Message("新增成功", AlertTypes.Success);
        }
        else
        {
            _updateBlogTypeModel =
                new Mapping<BlogTypePagingViewModel, UpdateBlogTypeModel>().Map(_dataModal.Data);

            await BlogCaller.BlogTypeService.UpdateAsync(_updateBlogTypeModel);

            Message("更新成功", AlertTypes.Success);
        }

        _dataModal.Hide();

        await FetchList();
    }

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        await FetchList(options.Page, options.ItemsPerPage);
    }
}
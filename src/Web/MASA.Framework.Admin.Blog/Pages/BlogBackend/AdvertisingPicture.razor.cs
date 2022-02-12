using MASA.Framework.Admin.Caller;

namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class AdvertisingPicture : ProCompontentBase
{
    [Inject] public BlogCaller BlogCaller { get; set; }

    #region table

    private GetBlogAdvertisingPicturesOption _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogAdvertisingPicturesListViewModel> _tableData;

    private readonly List<DataTableHeader<BlogAdvertisingPicturesListViewModel>> _headers = new()
    {
        new()
        { Text = "标题", Value = nameof(BlogAdvertisingPicturesListViewModel.Title), Sortable = false },
        new()
        { Text = "广告类型", Value = nameof(BlogAdvertisingPicturesListViewModel.Type), Sortable = false },
        new()
        { Text = "背景图片", Value = nameof(BlogAdvertisingPicturesListViewModel.Pic), Sortable = false },
        new()
        { Text = "排序", Value = nameof(BlogAdvertisingPicturesListViewModel.Sort), Sortable = false },
        new()
        { Text = "状态", Value = nameof(BlogAdvertisingPicturesListViewModel.Status), Sortable = false },
        new()
        { Text = "操作", Value = "Actions", Sortable = false }

    };

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        await FetchList(options.Page, options.ItemsPerPage);
    }

    private async Task FetchList(int pageIndex = 1, int pageSize = 10)
    {
        _options.PageIndex = pageIndex;
        _options.PageSize = pageSize;

        _loading = true;

        var pagingList = await BlogCaller.AdvertisingPicturesService.PagingAsync(_options);

        _tableData = pagingList.Data;
        _totalCount = pagingList.TotalCount;

        _loading = false;
    }


    #endregion

    #region from

    public string DataModalTitle { get; set; }

    private readonly DataModal<UpdateBlogAdvertisingPicturesModel> _dataModal = new();

    public void Create()
    {
        _dataModal.Show();
    }

    public void Modify(BlogAdvertisingPicturesListViewModel model)
    {
        // TODO: mapping
        var showModel = new UpdateBlogAdvertisingPicturesModel()
        {
            Id = model.Id,
            Title = model.Title,
            Pic = model.Pic,
            Type = model.Type,
            Sort = model.Sort,
            Status = model.Status
        };

        _dataModal.Show(showModel);
    }

    public async Task CreateOrUpdateAsync()
    {
        if (_dataModal.HasValue)
        {
            await BlogCaller.AdvertisingPicturesService.UpdateAsync(new()
            {
                Id = _dataModal.Data.Id,
                Title = _dataModal.Data.Title,
                Pic = _dataModal.Data.Pic,
                Type = _dataModal.Data.Type,
                Sort = _dataModal.Data.Sort,
                Status = _dataModal.Data.Status
            });
        }
        else
        {
            await BlogCaller.AdvertisingPicturesService.CreateAsync(new()
            {
                Title = _dataModal.Data.Title,
                Pic = _dataModal.Data.Pic,
                Type = _dataModal.Data.Type,
                Sort = _dataModal.Data.Sort,
                Status = _dataModal.Data.Status
            });

        }

        await FetchList();

        _dataModal.Hide();
    }

    public async Task RemoveAsync(BlogAdvertisingPicturesListViewModel model)
    {
        Confirm(
            title: "删除操作",
            content: $"您确认要删除（{model.Title}）吗？",
            onOk: async () =>
            {
                await BlogCaller.AdvertisingPicturesService.RemoveAsync(new Guid[] { model.Id });

                await FetchList();

                Message("删除成功", AlertTypes.Success);

            }, AlertTypes.Warning);

        StateHasChanged();

    }

    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await FetchList();

            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);
    }
}
using MASA.Framework.Admin.Caller;

namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class AdvertisingPicture : ProCompontentBase
{
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

        _tableData = new()
        {
            new()
            {
                Title = "测试2",
                Pic =
                    "https://img-cdn.lonsid.co/images/Promotion/Banner/832cd9c9-94a4-44d4-9b73-d90dbd3155d5.jpg?bannerTitle=春节快递停发公告",
                Type = 1,
                Sort = 1,
                Status = true
            }
        };

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

    public async Task ConfirmDataModal()
    {
        if (_dataModal.HasValue)
        {

        }
        else
        {
            _tableData.Add(new()
            {
                Title = _dataModal.Data.Title,
                Id = _dataModal.Data.Id,
                Pic = _dataModal.Data.Pic,
                Sort = _dataModal.Data.Sort,
                Status = _dataModal.Data.Status
            });
        }

        _totalCount = _tableData.Count;

        _dataModal.Hide();
        StateHasChanged();
    }

    #endregion

    [Inject] public BlogCaller blogCaller { get; set; }

    protected override async Task OnInitializedAsync()
    {

        await FetchList();

        _totalCount = _tableData.Count;

        await base.OnInitializedAsync();
    }
}
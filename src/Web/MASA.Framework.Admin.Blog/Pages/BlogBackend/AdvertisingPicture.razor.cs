namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class AdvertisingPicture : ProCompontentBase
{
    #region table

    private GetBlogAdvertisingPicturesOption _options = new();
    private int _totalCount = 0;
    private bool _loading = true;
    private List<BlogAdvertisingPicturesListViewModel> _tableData = new();

    private readonly List<DataTableHeader<BlogAdvertisingPicturesListViewModel>> _headers = new()
    {
        new DataTableHeader<BlogAdvertisingPicturesListViewModel>()
            { Text = "标题", Value = nameof(BlogAdvertisingPicturesListViewModel.Title), Sortable = false },
        new DataTableHeader<BlogAdvertisingPicturesListViewModel>()
            { Text = "广告类型", Value = nameof(BlogAdvertisingPicturesListViewModel.Type), Sortable = false },
        new DataTableHeader<BlogAdvertisingPicturesListViewModel>()
            { Text = "背景图片", Value = nameof(BlogAdvertisingPicturesListViewModel.Pic), Sortable = false },
        new DataTableHeader<BlogAdvertisingPicturesListViewModel>()
            { Text = "排序", Value = nameof(BlogAdvertisingPicturesListViewModel.Sort), Sortable = false },
        new DataTableHeader<BlogAdvertisingPicturesListViewModel>()
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

        // TODO: http

        _loading = false;
    }

    #endregion
}
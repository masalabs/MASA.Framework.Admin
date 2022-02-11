namespace MASA.Framework.Admin.Blog.Pages.BlogBackend;

public partial class Category : ProCompontentBase
{
    private GetBlogTypePagingOption _options = new();
    private int _totalCount = 0;
    private bool _loading = false;
    private List<BlogTypeListViewModel> _tableData = new()
    {
        new BlogTypeListViewModel()
        {
            Id = new Guid(),
            TypeName = "Net Core",
            CreationTime = DateTime.Now
        },
        new BlogTypeListViewModel()
        {
            Id = new Guid(),
            TypeName = "Go",
            CreationTime = DateTime.Now
        },
        new BlogTypeListViewModel()
        {
            Id = new Guid(),
            TypeName = "Python",
            CreationTime = DateTime.Now
        },
        new BlogTypeListViewModel()
        {
            Id = new Guid(),
            TypeName = "C++",
            CreationTime = DateTime.Now
        }
    };

    private readonly List<DataTableHeader<BlogTypeListViewModel>> _headers = new()
    {
        new()
        { Text = "名称", Value = nameof(BlogTypeListViewModel.TypeName), Sortable = false },
        new()
        { Text = "创建时间", Value = nameof(BlogTypeListViewModel.CreationTime),Sortable = false },
        new()
        { Text = "操作", Value = "actions", Width = 300, Sortable = false }
    };

    private DataModal<BlogTypeListViewModel> _dataModal = new();
    private CreateBlogTypeModel _createBlogTypeModel = new();
    private UpdateBlogTypeModel _updateBlogTypeModel = new();
    private string _dialogTitle = string.Empty;

    private async Task FetchList(int pageIndex = 1, int pageSize = 10)
    {
        _options.PageIndex = pageIndex;
        _options.PageSize = pageSize;

        _loading = true;

        // TODO: http

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

    public async Task UpdateAsync(Guid id)
    {
        _dialogTitle = "更新";
        _dataModal.Visible = true;  
        //var result = await GetProductTypeAsync(id);
        //_dataModal.Show(new Mapping<GetProductTypeViewModel, ProductTypeListViewModel>().Map(result));
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    public async Task DelAsync(BlogTypeListViewModel model)
    {
        //Confirm(
        //    title: "删除产品类型",
        //    content: $"您确认要删除（{model.Name}）吗？",
        //    onOk: async () =>
        //    {
        //        var result = await ProductCaller.ProductService.DelProductTypeAsync(model.Id);

        //        if (result == Guid.Empty)
        //        {
        //            Message("操作失败", AlertTypes.Error);
        //            return;
        //        }

        //        Message("操作成功", AlertTypes.Success);

        //        await FetchList();
        //        StateHasChanged();
        //    }, AlertTypes.Warning);
    }

    private async Task Save()
    {
        var result = Guid.Empty;

        if (!_dataModal.HasValue)
        {
            //_addProductTypeModel =
            //    new Mapping<ProductTypeListViewModel, AddProductTypeModel>().Map(_dataModal.Data);
            //_addProductTypeModel.CreatorUserId = MasaUser.UserId;
            //_addProductTypeModel.LastModifierUserId = MasaUser.UserId;

            //result = await ProductCaller.ProductService.AddProductTypeAsync(_addProductTypeModel);
        }
        else
        {
            //_updateProductTypeModel =
            //    new Mapping<ProductTypeListViewModel, UpdateProductTypeModel>().Map(_dataModal.Data);
            //_updateProductTypeModel.LastModifierUserId = MasaUser.UserId;

            //result = await ProductCaller.ProductService.UpdateProductTypeAsync(_updateProductTypeModel);
        }

        if (result != Guid.Empty)
        {
            //Message("操作成功", AlertTypes.Success);
        }

        _dataModal.Hide();

        await FetchList();
        StateHasChanged();
    }

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        await FetchList(options.Page, options.ItemsPerPage);
    }
}
namespace Masa.Framework.Admin.RCL.RBAC;

public class MenuPage : ComponentPageBase
{
    public List<MenuItemResponse> Datas { get; set; } = new();

    public MenuItemResponse CurrentData { get; set; } = new();

    private ConfigurationCaller ConfigurationCaller { get; set; }

    public string? _search;
    public string? Search
    {
        get { return _search; }
        set
        {
            _search = value;
            QueryPageDatasAsync().ContinueWith(_ => Reload?.Invoke());
        }
    }

    public int PageIndex { get; set; } = 1;

    public int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize = value;
            QueryPageDatasAsync().ContinueWith(_ => Reload?.Invoke());
        }
    }

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<MenuItemResponse>> Headers { get; set; }

    public MenuPage(ConfigurationCaller configurationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        ConfigurationCaller = configurationCaller;
        Headers = new()
        {
            new() { Text = i18n.T("Menu.Name"), Value = nameof(MenuItemResponse.Name) },
            new() { Text = i18n.T("Icon"), Value = nameof(MenuItemResponse.Icon), Sortable = false },
            new() { Text = i18n.T("Sort"), Value = nameof(MenuItemResponse.Sort) },
            new() { Text = i18n.T("Describe"), Value = nameof(MenuItemResponse.Describe), Sortable = false },
            new() { Text = i18n.T("State"), Value = nameof(MenuItemResponse.Disabled) },
            new() { Text = i18n.T("Action"), Value = "Action", Sortable = false }
        };
    }

    public async Task QueryPageDatasAsync()
    {
        GlobalConfig.Lodding = true;
        var result = await ConfigurationCaller.GetItemsAsync(PageIndex, PageSize);
        if (result.Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            Datas = pageData.Items.ToList();
            Datas.Add(new MenuItemResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Code = "9527",
                Describe = "test",
                Icon = "mdi-trending-up",
                Url = "/menu/list",
                Sort = 1
            });
            OpenSuccessMessage("查询成功！");
        }
        GlobalConfig.Lodding = false;
    }

    public async Task AddOrUpdateAsync()
    {
        GlobalConfig.Lodding = true;
        var result = default(ApiResultResponseBase);
        if (CurrentData.Id == Guid.Empty)
        {
            CurrentData.Code = "123";
            CurrentData.Url = "";
            CurrentData.ParentName = "234";
            CurrentData.ParentId = Guid.NewGuid();
            result = await ConfigurationCaller.CreateAsync(new AddMenuRequest(CurrentData.Code, CurrentData.Name, CurrentData.Sort, CurrentData.ParentId, CurrentData.ParentName)
            {
                Describe = CurrentData.Describe,
                Url = CurrentData.Url,
                Icon = CurrentData.Icon,
            });
        }
        else
        {
            result = await ConfigurationCaller.EditAsync(new EditMenuRequest(CurrentData.Id, CurrentData.Name, CurrentData.Sort, CurrentData.ParentId, CurrentData.ParentName)
            {
                Describe = CurrentData.Describe,
                Url = CurrentData.Url,
                Icon = CurrentData.Icon,
            });
        }
        GlobalConfig.Lodding = false;
    }

    public async Task DeleteAsync()
    {
        GlobalConfig.Lodding = true;
        var result = await ConfigurationCaller.DeleteAsync(CurrentData.Id);
       
        if (result.Success is false)
        {
            OpenErrorDialog(result.Message);         
        }
        else
        {

            await QueryPageDatasAsync();
        }
        GlobalConfig.Lodding = false;
    }
}


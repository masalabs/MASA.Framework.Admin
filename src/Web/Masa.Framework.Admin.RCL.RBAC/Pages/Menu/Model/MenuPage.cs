namespace Masa.Framework.Admin.RCL.RBAC;

public class MenuPage : ComponentPage
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

    public bool Lodding { get; set; }

    public bool Error { get; set; }

    public string? Message { get; set; }

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<MenuItemResponse>> Headers { get; set; }

    public MenuPage(ConfigurationCaller configurationCaller, I18n i18n)
    {
        ConfigurationCaller = configurationCaller;
        Headers = new()
        {
            new() { Text = T("Menu.Name"), Value = nameof(MenuItemResponse.Name) },
            new() { Text = T("Icon"), Value = nameof(MenuItemResponse.Icon), Sortable = false },
            new() { Text = T("Sort"), Value = nameof(MenuItemResponse.Sort) },
            new() { Text = T("Describe"), Value = nameof(MenuItemResponse.Describe), Sortable = false },
            new() { Text = T("State"), Value = nameof(MenuItemResponse.Disabled) },
            new() { Text = T("Action"), Value = "Action", Sortable = false }
        };

        string T(string key)
        {
            return i18n.T(key) ?? key;
        }
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await ConfigurationCaller.GetItemsAsync(PageIndex, PageSize);
        Error = !result.Success;
        Message = result.Message;
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
        }
        Lodding = false;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        Reload?.Invoke();
        var result = default(ApiResultResponseBase);
        if (CurrentData.Id == Guid.Empty)
        {
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
        Error = !result.Success;
        Message = result.Message;
        Lodding = false;
    }

    public async Task DeleteAsync()
    {
        Lodding = true;
        var result = await ConfigurationCaller.DeleteAsync(CurrentData.Id);
        Error = !result.Success;
        Message = result.Message;
        Lodding = false;
    }
}


namespace Masa.Framework.Admin.RCL.RBAC;

public class MenuPage : ComponentPageBase
{
    public List<MenuItemResponse> AllDatas { get; set; } = new();

    public List<MenuItemResponse> Datas { get; set; } = new();

    public MenuItemResponse CurrentData { get; set; } = new();

    public List<MenuNav> MenuNavs { get; set; } = new();

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

    public bool IsOpenMenuForm { get; set; }

    public bool IsAdd => CurrentData.Id == Guid.Empty;

    public MenuPage(ConfigurationCaller configurationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        ConfigurationCaller = configurationCaller;
        Headers = new()
        {
            new() { Text = i18n.T("Menu.Name"), Value = nameof(MenuItemResponse.Name) },
            new() { Text = i18n.T("Code"), Value = nameof(MenuItemResponse.Code) },
            new() { Text = i18n.T("Icon"), Value = nameof(MenuItemResponse.Icon), Sortable = false },
            new() { Text = i18n.T("Url"), Value = nameof(MenuItemResponse.Url), Sortable = false },
            new() { Text = i18n.T("Sort"), Value = nameof(MenuItemResponse.Sort) },
            new() { Text = i18n.T("Describe"), Value = nameof(MenuItemResponse.Describe), Sortable = false },
            new() { Text = i18n.T("State"), Value = nameof(MenuItemResponse.Disabled) },
            new() { Text = i18n.T("ParentMenu"), Value = nameof(MenuItemResponse.ParentName) },
            new() { Text = i18n.T("Action"), Value = "Action", Sortable = false }
        };
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await ConfigurationCaller.GetItemsAsync(PageIndex, PageSize, Search);
        if (result.Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            Datas = pageData.Items.ToList();
        }
        Lodding = false;
    }

    public void OpenMenuForm(MenuItemResponse? item = null)
    {
        CurrentData = item?.Copy() ?? new();
        IsOpenMenuForm = true;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        CurrentData.ParentName = Datas.FirstOrDefault(d => d.Id == CurrentData.ParentId)?.Name;
        if (IsAdd)
        {
            result = await ConfigurationCaller.CreateAsync(new AddMenuRequest(CurrentData.Name, CurrentData.Code, CurrentData.Url, CurrentData.Sort, CurrentData.Disabled)
            {
                Describe = CurrentData.Describe,
                Icon = CurrentData.Icon,
                ParentName = CurrentData.ParentName,
                ParentId = CurrentData.ParentId,
            });

            await CheckApiResult(result, I18n.T("Added menu successfully"), result.Message);
        }
        else
        {
            result = await ConfigurationCaller.EditAsync(new EditMenuRequest(CurrentData.Id, CurrentData.Name, CurrentData.Url, CurrentData.Sort, CurrentData.Disabled)
            {
                Describe = CurrentData.Describe,
                Icon = CurrentData.Icon,
                ParentName = CurrentData.ParentName,
                ParentId = CurrentData.ParentId,
            });

            await CheckApiResult(result, I18n.T("Edit menu successfully"), result.Message);
        }
        Lodding = false;
    }

    public void OpenDeleteMenuDialog(MenuItemResponse item)
    {
        CurrentData = item.Copy();
        OpenDeleteConfirmDialog(DeleteMenuAsync);
    }

    async Task DeleteMenuAsync(bool confirm)
    {
        if (confirm)
        {
            Lodding = true;
            var result = await ConfigurationCaller.AnyChildAsync(CurrentData.Id);
            if (result.Data)
            {
                OpenDeleteConfirmDialog(DeleteAsync,$"{I18n.T("Menu")}{CurrentData.Name}{I18n.T("has submenus, do you still want to delete?")}");
            }
            else
            {
                await DeleteAsync(confirm);
            }
            Lodding = false;
        }        
    }

    public async Task DeleteAsync(bool confirm)
    {
        if(confirm)
        {
            Lodding = true;
            var result = await ConfigurationCaller.DeleteAsync(CurrentData.Id);
            await CheckApiResult(result, I18n.T("Delete menu successfully"), result.Message);
            Lodding = false;
        }
    }

    async Task CheckApiResult(ApiResultResponseBase result, string successMessage, string errorMessage)
    {
        if (result.Success is false) OpenErrorDialog(errorMessage);
        else
        {
            OpenSuccessMessage(successMessage);
            await QueryPageDatasAsync();
        }
    }

    public async Task GetAllMenus()
    {
        await GetAllDatas();
        MenuNavs = GetMenus(AllDatas);
    }

    public async Task GetAllDatas()
    {
        Lodding = true;
        var result = await ConfigurationCaller.GetAllAsync();
        if (result.Success)
        {
            AllDatas = result.Data ?? new();
        }
        Lodding = false;
    }

    List<MenuNav> GetMenus(List<MenuItemResponse> menus)
    {
        var navs = new List<MenuNav>();
        var menuNavs = menus.Select(m => new MenuNav(m.Id, m.ParentId, m.Url, m.Icon, m.Name, m.Sort)).OrderBy(m => m.Sort).ToList();   
        navs.AddRange(menuNavs.Where(m => m.ParentId is null));
        foreach(var nav in navs)
        {
            BindChild(nav);
        }

        return navs;

        void BindChild(MenuNav nav)
        {
            var childs = menuNavs.Where(n => n.ParentId == nav.Id).ToArray();
            if(childs.Count() > 0)
            {
                nav.Children = childs;
                foreach(var child in childs)
                {
                    BindChild(child);
                }
            }
        }
    }

    public List<MenuItemResponse> GetMenuSelect()
    {
        var childMenus = new List<MenuItemResponse>();
        if (IsAdd) return AllDatas;
        else
        {
            AddChildMenu(CurrentData);
            return AllDatas.Where(m => m.Id != CurrentData.Id && childMenus.All(cm => cm.Id != m.Id)).ToList();
        }

        void AddChildMenu(MenuItemResponse menu)
        {
            var childs = AllDatas.Where(m => m.ParentId == menu.Id).ToList();
            if(childs.Count>0)
            {
                childMenus.AddRange(childs);
                foreach (var child in childs)
                {
                    AddChildMenu(child);
                }
            }          
        }
    }
}


public class MenuNav
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string Name { get; set; }

    public int Sort { get; set; }

    public MenuNav[]? Children { get; set; }

    public MenuNav(Guid id, Guid? parentId, string? href, string? icon, string name, int sort, MenuNav[]? children = null)
    {
        Id = id;
        ParentId = parentId;
        Href = href;
        Icon = icon;
        Name = name;
        Sort = sort;
        Children = children;
    }
}

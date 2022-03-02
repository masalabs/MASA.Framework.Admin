namespace Masa.Framework.Admin.RCL.RBAC;

public class MenuPage : ComponentPageBase
{
    public List<MenuItemResponse> AllDatas { get; set; } = new();

    public MenuItemResponse CurrentData { get; set; } = new();

    public List<MenuNav> BottomLayerMenus { get; set; } = new();

    public List<MenuNav> MenuNavs { get; set; } = new();

    private ConfigurationCaller ConfigurationCaller { get; set; }

    public bool IsEdit => CurrentData.Id != Guid.Empty;

    public MenuPage(ConfigurationCaller configurationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        ConfigurationCaller = configurationCaller;
    }

    public async Task GetAllMenus()
    {
        Lodding = true;
        var result = await ConfigurationCaller.GetAllAsync();
        if (result.Success)
        {
            AllDatas = result.Data ?? new();
        }
        Lodding = false;
        MenuNavs = GetMenuNavs(AllDatas);
    }

    List<MenuNav> GetMenuNavs(List<MenuItemResponse> menus)
    {
        var navs = new List<MenuNav>();
        var menuNavs = menus.Select(m => new MenuNav(m.Id, m.Code,m.ParentId, m.Url, m.Icon, m.Name, m.Sort)).OrderBy(m => m.Sort).ToList();
        BottomLayerMenus = menuNavs.Where(m => menuNavs.All(d => m.Id != d.ParentId)).ToList();
        navs.AddRange(menuNavs.Where(m => m.ParentId is null));
        foreach (var nav in navs)
        {
            BindChild(nav);
        }

        return navs;

        void BindChild(MenuNav nav)
        {
            var childs = menuNavs.Where(n => n.ParentId == nav.Id).ToArray();
            if (childs.Count() > 0)
            {
                nav.Children = childs;
                foreach (var child in childs)
                {
                    BindChild(child);
                }
            }
        }
    }

    public async Task AddOrUpdateAsync(MenuItemResponse? item = null)
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        var input = item ?? CurrentData;
        if (input.Id == Guid.Empty)
        {
            var request = new AddMenuRequest(input.Code, input.Name, input.Describe, input.Icon, input.ParentId, input.Url, input.Sort, input.Disabled,input.OnlyJump);
            result = await ConfigurationCaller.CreateAsync(request);
            await CheckApiResult(result, I18n.T("Added menu successfully"), result.Message);
        }
        else
        {
            var request = new EditMenuRequest(input.Id, input.Name, input.Describe, input.Icon, input.ParentId, input.Url, input.Sort, input.Disabled, input.OnlyJump);
            result = await ConfigurationCaller.EditAsync(request);
            await CheckApiResult(result, I18n.T("Edit menu successfully"), result.Message);
        }
        Lodding = false;
    }

    public async Task OpenDeleteMenuDialog(MenuItemResponse item)
    {
        CurrentData = item.Copy();
        Lodding = true;
        var result = await ConfigurationCaller.AnyChildAsync(CurrentData.Id);
        if (result.Data)
        {
            OpenDeleteConfirmDialog(DeleteAsync, $"{I18n.T("Menu")} {CurrentData.Name} {I18n.T("has submenus, do you still want to delete?")}");
        }
        else
        {
            OpenDeleteConfirmDialog(DeleteAsync);
        }
        Lodding = false;
    }

    public async Task DeleteAsync(bool confirm)
    {
        if(confirm)
        {
            Lodding = true;
            var result = await ConfigurationCaller.DeleteByIdsAsync(GetAllChildIds().ToArray());
            await CheckApiResult(result, I18n.T("Delete menu successfully"), result.Message);
            var firstLevelMenus = AllDatas.Where(m => m.ParentId == CurrentData.ParentId).OrderByDescending(m => m.Sort);
            var frontMenu = firstLevelMenus.FirstOrDefault(m => m.Sort <= CurrentData.Sort);
            CurrentData = (frontMenu ?? firstLevelMenus.LastOrDefault(m => m.Sort >= CurrentData.Sort) ?? AllDatas.FirstOrDefault(m => m.Id == CurrentData.ParentId) ?? new()).Copy();
            Lodding = false;
        }
    }

    public List<Guid> GetAllChildIds()
    {
        var result = new List<Guid>();
        result.Add(CurrentData.Id);
        var childs= AllDatas.Where(m => m.ParentId == CurrentData.Id).ToList();
        if(childs.Count >0)
        {
            RecursionChilds(CurrentData);
        }
        return result;

        void RecursionChilds(MenuItemResponse menu)
        {
            result.Add(menu.Id);
            var childs = AllDatas.Where(m => m.ParentId == menu.Id).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    RecursionChilds(child);
                }                   
            }
        }
    }

    async Task CheckApiResult(ApiResultResponseBase result, string successMessage, string errorMessage)
    {
        if (result.Success is false) OpenErrorDialog(errorMessage);
        else
        {
            OpenSuccessMessage(successMessage);
            await GetAllMenus();         
        }
    }
}


public class MenuNav
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public Guid? ParentId { get; set; }

    public string? Href { get; set; }

    public string? Icon { get; set; }

    public string Name { get; set; }

    public int Sort { get; set; }

    public bool Select { get; set; }

    public MenuNav[]? Children { get; set; }

    public MenuNav(Guid id,string code, Guid? parentId, string? href, string? icon, string name, int sort, MenuNav[]? children = null)
    {
        Id = id;
        Code = code;
        ParentId = parentId;
        Href = href;
        Icon = icon;
        Name = name;
        Sort = sort;
        Children = children;
    }
}

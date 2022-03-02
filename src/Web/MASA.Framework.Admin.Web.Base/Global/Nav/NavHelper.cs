namespace Masa.Framework.Admin.Web.Base.Global;

public class NavHelper
{
    private NavigationManager _navigationManager;
    private GlobalConfig _globalConfig;
    private PermissionHelper _permissionHelper;

    public List<NavModel> Navs { get; private set; } = new();

    public List<NavModel> SameLevelNavs { get; private set; } = new();

    public List<NavModel> BottomLevelNavs { get; private set; } = new();

    public List<PageTabItem> PageTabItems { get; private set; } = new();

    public NavHelper(NavigationManager navigationManager, GlobalConfig globalConfig, PermissionHelper permissionHelper)
    {
        _navigationManager = navigationManager;
        _globalConfig = globalConfig;
        _permissionHelper = permissionHelper;
    }

    public async Task InitializationAsync()
    {
        await _permissionHelper.InitializationMenusAsync();
        var menuNavs = _permissionHelper.Menus.Select(m => new NavModel(m.Id, m.Code, m.Url, m.Icon, m.Name, m.Sort, m.OnlyJump, m.Disabled, m.ParentId, null)).OrderBy(m => m.Sort).ToList();

        Navs = GetMenuNavs(menuNavs);

        BottomLevelNavs = menuNavs.Where(m => menuNavs.All(mn => m.Id != mn.ParentId)).ToList();

        SameLevelNavs = menuNavs;

        PageTabItems.Clear();
        SameLevelNavs.Where(nav => nav.Href is not null && nav.Href != "").ForEach(nav =>
          {
              PageTabItems.Add(new PageTabItem(nav.Title, nav.Href, nav.ParentIcon, PageTabsMatch.Prefix));
          });
    }

    List<NavModel> GetMenuNavs(List<NavModel> menuNavs)
    {
        var navs = new List<NavModel>();
        navs.AddRange(menuNavs.Where(m => m.ParentId is null));
        foreach (var nav in navs)
        {
            BindChild(nav);
        }

        return navs;

        void BindChild(NavModel nav)
        {
            var childs = menuNavs.Where(n => n.ParentId == nav.Id).ToArray();
            if (childs.Count() > 0)
            {
                nav.Children = childs;
                foreach (var child in childs)
                {
                    child.ParentId = nav.Id;
                    child.FullTitle = $"{nav.FullTitle} {child.Title}";
                    child.ParentIcon = nav.Icon;
                    BindChild(child);
                }
            }
        }
    }

    public void NavigateTo(NavModel nav)
    {
        Active(nav);
        _navigationManager.NavigateTo(nav.Href ?? "");
    }

    public void NavigateTo(string href)
    {
        var nav = SameLevelNavs.FirstOrDefault(n => n.Href == href);
        if (nav is not null) Active(nav);
        _navigationManager.NavigateTo(href);
    }

    public void RefreshRender(NavModel nav)
    {
        Active(nav);
        _globalConfig.CurrentNav = nav;
    }

    public void NavigateToByEvent(NavModel nav)
    {
        RefreshRender(nav);
        _navigationManager.NavigateTo(nav.Href ?? "");
    }

    public void Active(NavModel nav)
    {
        SameLevelNavs.ForEach(n => n.Active = false);
        nav.Active = true;
        //if (nav.ParentId != null) SameLevelNavs.First(n => n.Id == nav.ParentId).Active = true;
    }
}


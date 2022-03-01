using Masa.Framework.Sdks.Authentication.Response.Configuration;

namespace Masa.Framework.Admin.Web.Base.Global;

public class NavHelper
{
    private NavigationManager _navigationManager;
    private GlobalConfig _globalConfig;

    public List<NavModel> Navs { get; } = new();

    public List<NavModel> SameLevelNavs { get; } = new();

    public List<PageTabItem> PageTabItems { get; } = new();

    public NavHelper(NavigationManager navigationManager, GlobalConfig globalConfig)
    {
        _navigationManager = navigationManager;
        _globalConfig = globalConfig;
    }

    public void Initialization(List<MenuItemResponse> allMenus)
    {
        var allMenuNavs = allMenus.Select(m => new NavModel(m.Id, m.Code, m.Url, m.Icon, m.Name, m.Sort, m.ParentId, null)).OrderBy(m => m.Sort).ToList();

        if (_globalConfig.IsAdmin)
        {
            var adminNav = new NavModel(Guid.NewGuid(), "00000", "", "mdi-file-outline", "System Configuration", 0, null, null);
            var menuNav = new NavModel(Guid.NewGuid(), "000001", "menu/list", "", "Menu", 0, adminNav.ParentId, null);
            menuNav.FullTitle = adminNav.Title + " " + menuNav.Title;
            var permissionNav = new NavModel(Guid.NewGuid(), "000002", "permission", "", "Permission", 0, adminNav.ParentId, null);
            permissionNav.FullTitle = adminNav.Title + " " + permissionNav.Title;
            adminNav.Children = new NavModel[] { menuNav, permissionNav };
            Navs.Add(adminNav);
            Navs.AddRange(GetMenuNavs(allMenuNavs));
        }
        else
        {
            var menuCodes = _globalConfig.Permissions.Where(p => p.Resource == "menus")
                                     .SelectMany(p => p.Scope.Split(','))
                                     .Distinct();           
            DeleteEmptyMenuNavs(allMenuNavs, menuCodes);
            Navs.AddRange(GetMenuNavs(allMenuNavs));
        }

        SameLevelNavs.AddRange(allMenuNavs.Where(m => allMenuNavs.All(d => m.Id != d.ParentId)).ToList());

        SameLevelNavs.Where(nav => nav.Href is not null).ForEach(nav =>
        {
            PageTabItems.Add(new PageTabItem(nav.Title, nav.Href, nav.ParentIcon));
        });
    }

    void DeleteEmptyMenuNavs(List<NavModel> meunNavs, IEnumerable<string> menuCodes)
    {
        var deleteMenus = meunNavs.Where(m => meunNavs.All(d => m.Id != d.ParentId) && !menuCodes.Contains(m.Code)).ToList();
        if (deleteMenus.Count > 0)
        {
            foreach(var menu in deleteMenus)
            {
                meunNavs.Remove(menu);
            }
            DeleteEmptyMenuNavs(meunNavs, menuCodes);
        }
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


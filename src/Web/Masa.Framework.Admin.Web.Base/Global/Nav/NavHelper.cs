namespace Masa.Framework.Admin.Web.Base.Global;

public class NavHelper
{
    private PermissionHelper _permissionHelper;

    public List<NavModel> Navs { get; private set; } = new();

    public List<NavModel> SameLevelNavs { get; private set; } = new();

    public List<NavModel> BottomLevelNavs { get; private set; } = new();

    public List<PageTabItem> PageTabItems { get; private set; } = new();

    public NavHelper(PermissionHelper permissionHelper)
    {
        _permissionHelper = permissionHelper;
    }

    public async Task InitializationAsync()
    {
        await _permissionHelper.InitializationMenusAsync();
        var menuNavs = _permissionHelper.Menus.Select(m => new NavModel(m.Id, m.Code, m.Url, m.Icon, m.Name, m.Sort, m.OnlyJump, m.Disabled, m.ParentId, null, null)).OrderBy(m => m.Sort).ToList();

        Navs = GetMenuNavs(menuNavs);

        BottomLevelNavs = menuNavs.Where(m => menuNavs.All(mn => m.Id != mn.ParentId)).ToList();

        SameLevelNavs = menuNavs;

        PageTabItems.Clear();
        SameLevelNavs.Where(nav => nav.Href is not null).ForEach(nav =>
        {
            PageTabItems.Add(new PageTabItem(nav.Title, nav.Href, nav.Icon ?? nav.ParentNav?.Icon ?? "", PageTabsMatch.Prefix));
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
                    child.ParentNav = nav;
                    if (child.InheritIcon is null) child.InheritIcon = child.Icon ?? nav.Icon ?? nav.InheritIcon;

                    BindChild(child);
                }
            }
        }
    }
}


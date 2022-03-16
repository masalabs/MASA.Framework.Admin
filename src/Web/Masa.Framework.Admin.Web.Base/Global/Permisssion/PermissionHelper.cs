using Masa.Framework.Sdks.Authentication.Callers;
using Masa.Framework.Sdks.Authentication.Internal.Enum;
using Masa.Framework.Sdks.Authentication.Response.Configuration;

namespace Masa.Framework.Admin.Web.Base.Global;

public class PermissionHelper
{
    private bool IsAdmin { get; init; }

    List<AuthorizeItemResponse> MenuPermissions { get; init; } = new();

    List<AuthorizeItemResponse> ApiPermissions { get; init; } = new();

    public List<MenuItemResponse> Menus { get; private set; } = new();

    ConfigurationCaller ConfigurationCaller { get; init; }

    public PermissionHelper(IHttpContextAccessor httpContextAccessor, ConfigurationCaller configurationCaller)
    {
        ConfigurationCaller = configurationCaller;
        var permissionsJson = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Permissions")?.Value;
        if (permissionsJson is not null)
        {
            var permissions = JsonSerializer.Deserialize<List<AuthorizeItemResponse>>(permissionsJson);
            if (permissions is not null)
            {
                MenuPermissions = permissions.Where(p => p.ObjectType is ObjectType.Menu).ToList();
                ApiPermissions = permissions.Where(p => p.ObjectType is ObjectType.Api).ToList();
                var isAdmin = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value ?? "false";
                IsAdmin = Convert.ToBoolean(isAdmin);
            }
        }
    }

    public async Task InitializationMenusAsync()
    {
        var menusResponse = await ConfigurationCaller.GetAllAsync();
        Menus = menusResponse.Data ?? new();
        if (IsAdmin)
        {
            var indexNav = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "0001",
                Url = "dashboard/index",
                Icon = "mdi-trending-up",
                Name = "Dashboard",
                Sort = 0,
                ParentId = null,
            };
            var adminNav = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "00000",
                Url = "",
                Icon = "mdi-file-outline",
                Name = "Authentication",
                Sort = 1,
                ParentId = null,
            };
            var menuNav = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000001",
                Url = "menu/list",
                Icon = "",
                Name = "Menu",
                Sort = 1,
                ParentId = adminNav.Id,
            };
            var permissionNav = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000002",
                Url = "permission",
                Icon = "",
                Name = "Permission",
                Sort = 2,
                ParentId = adminNav.Id,
            };
            var roleDetailNav = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000003",
                Url = "role/details",
                Icon = "",
                Name = "roleDetail",
                Sort = 3,
                OnlyJump = true,
                ParentId = adminNav.Id,
            };
            Menus.Add(indexNav);
            Menus.Add(adminNav);
            Menus.Add(menuNav);
            Menus.Add(permissionNav);
            Menus.Add(roleDetailNav);
            var organization = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "00001",
                Url = "",
                Icon = "mdi-account-check-outline",
                Name = "Organization",
                Sort = 2,
                ParentId = null,
            };
            var org = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000011",
                Url = "org/index",
                Icon = "",
                Name = "Org",
                Sort = 1,
                ParentId = organization.Id,
            };
            var userList = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000012",
                Url = "user/list",
                Icon = "",
                Name = "User",
                Sort = 2,
                ParentId = organization.Id,
            };
            var usergroup = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000013",
                Url = "usergroup/list",
                Icon = "",
                Name = "Usergroup",
                Sort = 3,
                ParentId = organization.Id,
            };
            var usergroupDetail = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000014",
                Url = "usergroupDetail",
                Icon = "",
                Name = "usergroupDetail",
                Sort = 4,
                OnlyJump = true,
                ParentId = organization.Id,
            };
            var userDetail = new MenuItemResponse
            {
                Id = Guid.NewGuid(),
                Code = "000015",
                Url = "userDetail",
                Icon = "",
                Name = "UserDetail",
                Sort = 5,
                OnlyJump = true,
                ParentId = organization.Id,
            };
            Menus.Add(organization);
            Menus.Add(org);
            Menus.Add(userList);
            Menus.Add(usergroup);
            Menus.Add(usergroupDetail);
            Menus.Add(userDetail);
        }
        else
        {
            var menuCodes = MenuPermissions.Where(p => p.Resource == "menus")
                                        .SelectMany(p => p.Scope.Split(','))
                                        .Distinct();
            DeleteEmptyMenuNavs(Menus, menuCodes);
        }

        void DeleteEmptyMenuNavs(List<MenuItemResponse> meuns, IEnumerable<string> menuCodes)
        {
            var deleteMenus = meuns.Where(m => meuns.All(d => m.Id != d.ParentId) && !menuCodes.Contains(m.Code)).ToList();
            if (deleteMenus.Count > 0)
            {
                foreach (var menu in deleteMenus)
                {
                    meuns.Remove(menu);
                }
                DeleteEmptyMenuNavs(meuns, menuCodes);
            }
        }
    }

    public bool CheckMenuPermissionBuUrl(string menuUrl, string? action = null)
    {
        if (IsAdmin) return true;
        var menu = Menus.FirstOrDefault(m => m.Url != "" && menuUrl.Contains(m.Url));
        if (menu is null) return false;
        else return CheckMenuPermission(menu.Code, action);
    }

    public bool CheckMenuPermission(string menuCode, string? action = null)
    {
        if (IsAdmin) return true;
        else
        {
            var permission = MenuPermissions.Where(mp => mp.Scope.Contains(menuCode)).ToList();
            if (action is null) return permission.Count > 0;
            else
            {
                return permission.Any(p => p.Action.Contains(action));
            }
        }
    }

    public bool CheckApiPermission(string scope, string? action = null)
    {
        if (IsAdmin) return true;
        else
        {
            var permission = ApiPermissions.Where(mp => mp.Scope.Contains(scope)).ToList();
            if (action is null) return permission.Count > 0;
            else
            {
                return permission.Any(p => p.Action.Contains(action));
            }
        }
    }
}


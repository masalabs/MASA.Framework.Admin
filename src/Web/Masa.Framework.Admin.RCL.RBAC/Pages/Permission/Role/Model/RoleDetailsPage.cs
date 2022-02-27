namespace Masa.Framework.Admin.RCL.RBAC;

public class RoleDetailsPage : ComponentPageBase
{
    public RoleDetailResponse Detail { get; set; } = new();

    public List<RoleItemResponse> AllRoles { get; set; } = new();

    public List<RoleItemsResponse> AllRoleItems { get; set; } = new();

    public List<RoleItemResponse> SelectRoles { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public RolePage RolePage { get; set; }

    public NavigationManager NavigationManager { get; set; }

    public bool OpenAddUserRoleDialog { get; set; }

    public UserCaller UserCaller { get; set; }

    public List<UserSelect> UserSelect { get; set; } =new();

    public bool OpenAddAuthorizeDialog { get; set; }

    public bool OpenAddChildrenRolesDialog { get; set; }

    public List<DataTableHeader<RoleItemResponse>> ChildrenRoleHeaders { get; set; }

    public RoleDetailsPage(AuthenticationCaller authenticationCaller, UserCaller userCaller, NavigationManager navigationManager, RolePage rolePage, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        UserCaller = userCaller;
        NavigationManager = navigationManager;
        RolePage = rolePage;

        ChildrenRoleHeaders = new()
        {
            new() { Text = i18n.T("Role.Name"), Value = nameof(RoleItemResponse.Name) },
            new() { Text = i18n.T("Role.Number"), Value = nameof(RoleItemResponse.Number) },
            new() { Text = i18n.T("CreationTime"), Value = nameof(RoleItemResponse.CreationTime), Sortable = false },
            new() { Text = i18n.T("Describe"), Value = nameof(RoleItemResponse.Describe), Sortable = false },
        };
    }

    public async Task InitAsync(string? roleId)
    {
        await QueryRoleById(roleId);
        await SelectAllRoleItemsAsync();
        await SelectRoleAsync();
        await Task.CompletedTask;//userRole
    }

    public async Task QueryRoleById(string? roleId)
    {
        Guid? id = null;
        if (roleId is null)
        {
            if (RolePage.CurrentData.Id != Guid.Empty)
            {
                id = RolePage.CurrentData.Id;
            }
        }
        else id = Guid.Parse(roleId);

        if (id is null) return;
        Lodding = true;
        var result = await AuthenticationCaller.GetRoleDetailAsync(id.Value);
        if (result.Success)
        {
            Detail = result.Data ?? new();          
        }
        else
        {
            OpenErrorMessage(result.Message);
        }
        Lodding = false;
    }

    public async Task SelectRoleAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.SelectRoleAsync();
        if (result.Success)
        {
            AllRoles = result.Data ?? new();
            foreach(var role in AllRoles)
            {
                role.Select = false;
                if (Detail.ChildrenRoleIds.Contains(role.Id)) role.Select = true;
            }
            BuildSelectRoles();
        }
        else
        {
            OpenErrorMessage(result.Message);
        }
        Lodding = false;
    }

    public async Task SelectAllRoleItemsAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetAllRoleItemAsync();
        if (result.Success)
        {
            AllRoleItems = result.Data ?? new();
        }
        else
        {
            OpenErrorMessage(result.Message);
        }
        Lodding = false;
    }

    public async Task<bool> UpdateRoleInfoAsync(EditContext context)
    {
        if (context.Validate())
        {
            Lodding = true;
            var request = new EditRoleRequest(Detail.Id, Detail.Name, Detail.Describe);
            var result = await AuthenticationCaller.EditRoleAsync(request);
            CheckApiResult(result, I18n.T("Edit Role successfully"), result.Message);
            Lodding = false;
            return result.Success;
        }
        return false;
    }

    public async Task AddChildrenRolesAsync()
    {
        Lodding = true;
        var request = new AddChildRolesRequest(Detail.Id, AllRoles.Where(r => r.Select).Select(r => r.Id).ToList());
        var result = await AuthenticationCaller.AddChildrenRolesAsync(request);
        CheckApiResult(result, I18n.T("Add children roles successfully"), result.Message);
        Lodding = false;
    }

    void CheckApiResult(ApiResultResponseBase result, string successMessage, string errorMessage)
    {
        if (result.Success is false) OpenErrorDialog(errorMessage);
        else
        {
            OpenSuccessMessage(successMessage);
        }
    }

    void BuildSelectRoles()
    {
        var childRoles = GetChildRoledLoop(Detail.Id);
        var parentRoles = GetParentRoledLoop(Detail.Id);
        SelectRoles = AllRoles.Where(r => Detail.ChildrenRoleIds.Contains(r.Id) ||(!childRoles.Any(cr => cr == r.Id) && !parentRoles.Any(pr => pr == r.Id))).ToList();      

        List<Guid> GetChildRoledLoop(Guid roleId)
        {
            var childRoledLoop = new List<Guid>();
            var childRoleds = AllRoleItems.Where(r => r.ParentRoleId == roleId).Select(r => r.RoleId);
            foreach(var child in childRoleds)
            {
                childRoledLoop.AddRange(GetChildRoledLoop(child));
            }
            childRoledLoop.Add(roleId);

            return childRoledLoop;
        }

        List<Guid> GetParentRoledLoop(Guid roleId)
        {
            var parentRoledLoop = new List<Guid>();
            var parentRoleds = AllRoleItems.Where(r => r.RoleId == roleId).Select(r => r.ParentRoleId);
            foreach (var parent in parentRoleds)
            {
                parentRoledLoop.AddRange(GetParentRoledLoop(parent));
            }
            parentRoledLoop.Add(roleId);

            return parentRoledLoop;
        }
    }
}

public class UserSelect
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = "";

    public bool Select { get; set; }
}


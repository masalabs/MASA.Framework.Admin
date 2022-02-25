using MASA.Framework.Sdks.Authentication.Request.LogStatistics;

namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class View
{
    private StringNumber _tab;
    private UserDetailResponse _userDetail = new();
    private bool _addRoleDialog, _addGroupDialog;
    private List<SelectItem> _roleSelectItems = new(), _groupSelectItems = new();
    private List<RoleItemResponse> _userRoles = new();
    private List<UserGroupItemResponse> _userGroups = new();
    private string? _addRoleId, _addGroupId;
    private List<DataTableHeader<LoginRecord>> _loginRecordHeaders = new List<DataTableHeader<LoginRecord>>
    {
        new (){ Text= "登录时间", Sortable= false, Value= nameof(LoginRecord.LoginTime)},
        new (){ Text= "IP 地址", Sortable= false, Value= nameof(LoginRecord.IP),Align="center"},
        new (){ Text= "是否成功", Sortable= false, Value= nameof(LoginRecord.Success),Align="center"},
        new (){ Text= "浏览器", Sortable= false, Value= nameof(LoginRecord.Browser),Align="center"},
        new (){ Text= "地理位置", Sortable= false, Value= nameof(LoginRecord.Address),Align="center"}
    };
    private List<DataTableHeader<UserGroupItemResponse>> _userGroupHeaders = new List<DataTableHeader<UserGroupItemResponse>>
    {
        new (){ Text= "用户组名称", Sortable= false, Value= nameof(UserGroupItemResponse.Name)},
        new (){ Text= "简介", Sortable= false, Value= nameof(UserGroupItemResponse.Describtion)},
        new (){ Text= "操作", Sortable= false, Value= nameof(UserGroupItemResponse.Id)}
    };
    private List<DataTableHeader<RoleItemResponse>> _roleItemHeaders = new List<DataTableHeader<RoleItemResponse>> {
        new (){ Text= "角色名称", Sortable= false, Value= nameof(RoleItemResponse.Name)},
        new (){ Text= "角色描述", Sortable= false, Value= nameof(RoleItemResponse.Describe)},
        new (){ Text= "操作", Sortable= false, Value= nameof(RoleItemResponse.Id)}
    };

    [Inject]
    public UserCaller UserCaller { get; set; } = null!;

    [Inject]
    public AuthenticationCaller AuthenticationCaller { get; set; } = null!;

    [Inject]
    public UserGroupCaller UserGroupCaller { get; set; } = null!;

    [Inject]
    public LogStatisticsCaller LogStatisticsCaller { get; set; } = null!;

    [Parameter]
    public string? Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            return;
        }
        var dataRes = await UserCaller.GetDetailsAsync(Id);
        if (dataRes.Success && dataRes.Data != null)
        {
            _userDetail = dataRes.Data;
        }

        await LoadUserRoles();
        await LoadUserGroups();
    }

    private async Task OpenRoleDialog()
    {
        var dataRes = await AuthenticationCaller.SelectRoleAsync();
        if (!dataRes.Success || dataRes.Data == null)
        {
            //获取失败
            return;
        }
        _addRoleDialog = true;
        _roleSelectItems = dataRes.Data.Select(role => new SelectItem
        {
            Name = role.Name,
            Describetion = role.Describe ?? "",
            Id = role.Id
        }).ToList();
    }
    private async Task OpenGroupDialog()
    {
        var dataRes = await UserGroupCaller.SelectUserGroupsAsync();
        if (!dataRes.Success || dataRes.Data == null)
        {
            //获取失败
            return;
        }
        _addGroupDialog = true;
        _groupSelectItems = dataRes.Data.Select(userGroup => new SelectItem
        {
            Name = userGroup.Name,
            Describetion = userGroup.Describtion,
            Id = userGroup.Id
        }).ToList();
    }


    private async Task AddUserRole()
    {
        if (string.IsNullOrEmpty(_addRoleId) || string.IsNullOrEmpty(Id))
        {
            //tip msg
            return;
        }
        _addRoleDialog = false;
        await UserCaller.CreateRoleAsync(new CreateUserRoleRequest
        {
            RoleId = Guid.Parse(_addRoleId),
            UserId = Guid.Parse(Id)
        });
        await LoadUserRoles();
    }

    private async Task AddUserGroup()
    {
        if (string.IsNullOrEmpty(_addGroupId) || string.IsNullOrEmpty(Id))
        {
            //tip msg
            return;
        }
        _addGroupDialog = false;
        await UserCaller.CreateUserGroupAsync(new CreateUserGroupRequest
        {
            UserGroupId = Guid.Parse(_addGroupId),
            UserId = Guid.Parse(Id)
        });
        await LoadUserGroups();
    }

    private async Task RemoveUserGroup(Guid groupId)
    {
        await UserCaller.RemoveUserGroupAsync(new RemoveUserGroupRequest
        {
            UserGroupId = groupId,
            UserId = Guid.Parse(Id)
        });
        await LoadUserGroups();
    }

    private async Task LoadUserRoles()
    {
        var userRolesRes = await UserCaller.GetUserRolesAsync(Guid.Parse(Id));
        if (!userRolesRes.Success || userRolesRes.Data == null)
        {
            return;
        }
        var rolesRes = await AuthenticationCaller.GetRolesByIdsAsync(userRolesRes.Data.Select(a => a.RoleId).ToList());
        if (!rolesRes.Success || rolesRes.Data == null)
        {
            return;
        }
        _userRoles = rolesRes.Data;
    }

    private async Task LoadUserGroups()
    {
        var userGroupsRes = await UserGroupCaller.GetUserGroupsAsync(Guid.Parse(Id));
        if (!userGroupsRes.Success || userGroupsRes.Data == null)
        {
            return;
        }
        _userGroups = userGroupsRes.Data;
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        await LogStatisticsCaller.CreateLogAsync(new OperationLogCreateRequest
        {
            Description = "访问了用户详情页面",
            OperationLogType = OperationLogType.VisitPage
        });
        await base.OnAfterRenderAsync(firstRender);
    }
}


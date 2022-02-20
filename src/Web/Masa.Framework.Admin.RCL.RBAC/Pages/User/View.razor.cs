using MASA.Framework.Admin.Contracts.Authentication.Old.Response;

namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class View
{
    private StringNumber _tab;
    private UserDetailResponse _userDetail = new();
    private bool _addRoleDialog = false;
    private List<RoleSelectItem> _roleSelectItems = new();
    private List<RoleItemResponse> _userRoles = new();
    private string? _addRoleId;
    private List<DataTableHeader<LoginRecord>> _loginRecordHeaders = new List<DataTableHeader<LoginRecord>>
    {
        new (){ Text= "登录时间", Sortable= false, Value= nameof(LoginRecord.LoginTime)},
        new (){ Text= "IP 地址", Sortable= false, Value= nameof(LoginRecord.IP),Align="center"},
        new (){ Text= "是否成功", Sortable= false, Value= nameof(LoginRecord.Success),Align="center"},
        new (){ Text= "浏览器", Sortable= false, Value= nameof(LoginRecord.Browser),Align="center"},
        new (){ Text= "地理位置", Sortable= false, Value= nameof(LoginRecord.Address),Align="center"}
    };
    private List<DataTableHeader<UserGroup>> _userGroupHeaders = new List<DataTableHeader<UserGroup>>
    {
        new (){ Text= "用户组名称", Sortable= false, Value= nameof(UserGroup.Name)},
        new (){ Text= "简介", Sortable= false, Value= nameof(UserGroup.Description)},
        new (){ Text= "操作", Sortable= false, Value= nameof(UserGroup.Id)}
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
        _roleSelectItems = dataRes.Data.Select(role => new RoleSelectItem
        {
            Name = role.Name,
            Describetion = role.Describe ?? "",
            Id = role.Id
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
}


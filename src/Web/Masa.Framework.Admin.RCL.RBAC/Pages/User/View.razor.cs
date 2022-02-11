namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class View
{
    private StringNumber _tab;
    private readonly UserDetailResponse _userDetail = new();
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
        new (){ Text= "组名", Sortable= false, Value= nameof(UserGroup.Name)},
        new (){ Text= "简介", Sortable= false, Value= nameof(UserGroup.Description)},
        new (){ Text= "操作", Sortable= false, Value= nameof(UserGroup.Id)}
    };

    [Inject]
    public UserCaller UserCaller { get; set; } = null!;

    [Parameter]
    public string? Id { get; set; }

}


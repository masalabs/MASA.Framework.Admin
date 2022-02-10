namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class View
{
    private readonly UserDetailResponse _userDetail = new();
    private List<DataTableHeader<LoginRecord>> _loginRecordHeaders = new List<DataTableHeader<LoginRecord>>
    {
        new (){ Text= "��¼ʱ��", Sortable= false, Value= nameof(LoginRecord.LoginTime)},
        new (){ Text= "IP ��ַ", Sortable= false, Value= nameof(LoginRecord.IP),Align="center"},
        new (){ Text= "�Ƿ�ɹ�", Sortable= false, Value= nameof(LoginRecord.Success),Align="center"},
        new (){ Text= "�����", Sortable= false, Value= nameof(LoginRecord.Browser),Align="center"},
        new (){ Text= "����λ��", Sortable= false, Value= nameof(LoginRecord.Address),Align="center"}
    };

    [Parameter]
    public string? Id { get; set; }

}


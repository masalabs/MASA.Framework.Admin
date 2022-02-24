namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class List
{
    private bool _visible;
    private bool _valid = true, _snackbar = false;
    private MForm _form = new();
    private PaginationPage<UserItemResponse> _pageData = new();
    private UserItemResponse _userItem = new();
    private CreateUserModel _createUserModel = new();
    private List<int> _pageSizes = new() { 10, 25, 50, 100 };
    private readonly List<DataTableHeader<UserItemResponse>> _headers = new()
    {
        new() { Text = "Account", Value = nameof(UserItemResponse.Account), CellClass = "" },
        new() { Text = "Name", Value = nameof(UserItemResponse.Name) },
        new() { Text = "Email", Value = nameof(UserItemResponse.Email) },
        new() { Text = "State", Value = nameof(UserItemResponse.State) },
        new() { Text = "Gender", Value = nameof(UserItemResponse.Gender) },
        new() { Text = "LastLoginTime", Value = nameof(UserItemResponse.LastLoginTime) },
        new() { Text = "Action", Value = "Action", Sortable = false }
    };
    private List<StateItem> _selectStateList => new List<StateItem>
    {
        new StateItem((int)State.Enable,State.Enable.ToString()),
        new StateItem((int)State.Disabled,State.Disabled.ToString()),
    };

    [Inject]
    public UserCaller UserCaller { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        var dataRes = await UserCaller.GetListAsync(_pageData.PageIndex, _pageData.PageSize, _pageData.Name ?? "", _pageData.State);

        if (dataRes.Success && dataRes.Data != null)
        {
            _pageData.PageData = dataRes.Data.Items.ToList();
            _pageData.CurrentCount = dataRes.Data.Count;
        }
    }

    private string GetInitialShow(string name)
    {
        return string.Join("", name.Split(' ').Select(n => n[0].ToString().ToUpper()));
    }

    private void NavToDetails(string id)
    {
        Nav.NavigateTo($"/user/view/{id}");
    }

    private async Task DeleteUser(string id)
    {
        var res = await UserCaller.DeleteAsync(id);
        if (!res.Success)
        {
            //tip msg
        }
        else
        {
            //reload items
        }
    }

    private async Task CreateUser(EditContext context)
    {
        var success = context.Validate();
        if (!success)
        {
            return;
        }
        if (!_createUserModel.Pwd.Trim().Equals(_createUserModel.ConfirmPwd.Trim()))
        {
            //密码确认失败 提示
            return;
        }

        var res = await UserCaller.CreateAsync(_createUserModel);
        if (!res.Success)
        {
            _snackbar = true;
        }

        _visible = false;
        await LoadData();
    }
}


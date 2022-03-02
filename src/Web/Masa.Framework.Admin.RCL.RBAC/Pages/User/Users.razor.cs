namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class Users
{
    private bool _visible;
    private bool _valid = true;
    private MForm _form = new();
    private PaginationPage<UserItemResponse> _pageData = new();
    private UserItemResponse _userItem = new();
    private CreateUserModel _createUserModel = new();
    private List<int> _pageSizes = new() { 10, 25, 50, 100 };
    private readonly List<DataTableHeader<UserItemResponse>> _headers = new()
    {
        new() { Text = "账号", Value = nameof(UserItemResponse.Account), CellClass = "" },
        new() { Text = "姓名", Value = nameof(UserItemResponse.Name) },
        new() { Text = "邮箱", Value = nameof(UserItemResponse.Email) },
        new() { Text = "状态", Value = nameof(UserItemResponse.State) },
        new() { Text = "性别", Value = nameof(UserItemResponse.Gender) },
        new() { Text = "最后登录时间", Value = nameof(UserItemResponse.LastLoginTime) },
        new() { Text = "操作", Value = "Action", Sortable = false }
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
        HandleCaller(dataRes, (data) =>
        {
            if (data != null)
            {
                _pageData.PageData = data.Items.ToList();
                _pageData.CurrentCount = data.Count;
            }
        });
    }

    private string GetInitialShow(string name)
    {
        return string.Join("", name.Split(' ').Select(n => n[0].ToString().ToUpper()));
    }

    private void NavToDetails(string id)
    {
        Nav.NavigateTo($"/user/{id}");
    }

    private async Task DeleteUser(string id)
    {
        await HandleCallerAsync(await UserCaller.DeleteAsync(id), async () =>
        {
            await LoadData();
        });
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
            GlobalConfig.OpenMessage(I18n.T("ConfirmPasswordError"), MessageType.Error);
            return;
        }
        await HandleCallerAsync(await UserCaller.CreateAsync(_createUserModel), async () =>
        {
            _visible = false;
            await LoadData();
        });
    }
}


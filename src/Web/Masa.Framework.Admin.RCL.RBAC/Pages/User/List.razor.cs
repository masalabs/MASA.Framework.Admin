using MASA.Blazor;
using MASA.Framework.Admin.Caller.Callers;
using Microsoft.AspNetCore.Components.Forms;

namespace Masa.Framework.Admin.RCL.RBAC.Pages.User;

public partial class List
{
    private bool _visible;
    private bool _valid = true, _snackbar = false;
    private MForm _form = new();
    private UserPage _userPage = new();
    private UserItemResponse _userItem = new();
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
    public UserCaller UserCaller { get; set; }

    private string GetInitialShow(string name)
    {
        return string.Join("", name.Split(' ').Select(n => n[0].ToString().ToUpper()));
    }

    private void NavToDetails(string id)
    {
        Nav.NavigateTo($"/user/view/{id}");
    }

    private void DeleteUser(string id)
    {

    }

    private async void CreateUser(EditContext context)
    {
        var success = context.Validate();
        if (!success)
        {
            return;
        }

        var res = await UserCaller.CreateAsync("", "");
        if (!res.Success)
        {
            _snackbar = true;
        }

        _visible = false;
    }
}


namespace Masa.Framework.Admin.Rcl.Rbac.Pages.User;

public partial class UserGroupView
{
    bool? _permissionDialog;
    List<UserItemResponse> _groupUsers = new();
    List<PermissionItemResponse> _groupPermissions = new();
    List<DataTableHeader<UserItemResponse>> _userHeaders = new List<DataTableHeader<UserItemResponse>>
    {
        new (){ Text= "名称", Sortable= false, Value= nameof(UserItemResponse.Name)},
        new (){ Text= "邮箱", Sortable= false, Value= nameof(UserItemResponse.Email)},
        new (){ Text= "操作", Sortable= false, Value= nameof(UserItemResponse.Id)}
    };
    List<DataTableHeader<PermissionItemResponse>> _permissionHeaders = new List<DataTableHeader<PermissionItemResponse>> {
        new (){ Text= "名称", Sortable= false, Value= nameof(PermissionItemResponse.Name)},
        new (){ Text= "资源", Sortable= false, Value= nameof(PermissionItemResponse.Resource)},
        new (){ Text= "操作", Sortable= false, Value= nameof(PermissionItemResponse.Id)}
    };

    [Parameter]
    public string Id { get; set; } = Guid.Empty.ToString();

    public Guid ID { get { return Guid.Parse(Id); } }

    [Inject]
    public AuthenticationCaller AuthenticationCaller { get; set; } = null!;

    [Inject]
    public UserGroupCaller UserGroupCaller { get; set; } = null!;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadUsersAysnc();
            await LoadPermissionAsync();
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task LoadPermissionAsync()
    {
        var res = await UserGroupCaller.GetPermissionIdsAsync(ID);
        if (!res.Success || res.Data == null)
        {
            return;
        }
        var resPermissions = await AuthenticationCaller.GetPermissionsByIds(res.Data);
        HandleCaller(resPermissions, (data) =>
        {
            _groupPermissions = data;
        });
    }

    private async Task LoadUsersAysnc()
    {
        HandleCaller(await UserGroupCaller.GetUsersAsync(ID), (data) =>
        {
            _groupUsers = data;
        });
    }

    private async Task AddAuthorize(AuthorizeItemResponse data)
    {
        var permission = new AddPermissionRequest()
        {
            ObjectType = data.ObjectType,
            Name = data.PermissionName,
            Resource = data.Resource,
            Scope = data.Scope,
            UserGroupId = ID,
            PermissionType = data.PermissionType,
            Action = data.Action,
        };
        var result = await AuthenticationCaller.AddPermissionAsync(permission);
        await HandleCallerAsync(result, async () =>
        {
            await LoadPermissionAsync();
        });
    }

    private async Task RemovePermissionAsync(Guid permissionId)
    {
        var res = await UserGroupCaller.RemovePermissionAsync(new RemoveGroupPermissionRequest
        {
            PermissionId = permissionId,
            UserGroupId = ID
        });
        await HandleCallerAsync(res, async () =>
        {
            await LoadPermissionAsync();
        });
    }

}


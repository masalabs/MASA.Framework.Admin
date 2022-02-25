namespace Masa.Framework.Admin.RCL.RBAC;

public class RoleDetailsPage : ComponentPageBase
{
    public RoleDetailResponse Detail { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public RolePage RolePage { get; set; }

    public NavigationManager NavigationManager { get; set; }

    public bool OpenAddUserRoleDialog { get; set; }

    public UserCaller UserCaller { get; set; }

    public List<UserSelect> UserSelect { get; set; } =new();

    public bool OpenAddAuthorizeDialog { get; set; }

    public bool OpenAddChildRoleDialog { get; set; }

    public RoleDetailsPage(AuthenticationCaller authenticationCaller, UserCaller userCaller, NavigationManager navigationManager, RolePage rolePage, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        UserCaller = userCaller;
        NavigationManager = navigationManager;
        RolePage = rolePage;
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

    void CheckApiResult(ApiResultResponseBase result, string successMessage, string errorMessage)
    {
        if (result.Success is false) OpenErrorDialog(errorMessage);
        else
        {
            OpenSuccessMessage(successMessage);
        }
    }
}

public class UserSelect
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = "";

    public bool Select { get; set; }
}


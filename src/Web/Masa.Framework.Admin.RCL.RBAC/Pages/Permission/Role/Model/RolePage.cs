using MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

namespace Masa.Framework.Admin.RCL.RBAC;

public class RolePage : ComponentPageBase
{
    public List<RoleItemResponse> Datas { get; set; } = new();

    public RoleItemResponse CurrentData { get; set; } = new();

    public List<AuthorizeItemResponse> AuthorizeDatas { get; set; } = new();

    public AuthorizeItemResponse CurrentAuthorizeData { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public int State { get; set; } = -1;

    public string? _search;

    public string? Search
    {
        get { return _search; }
        set
        {
            _search = value;
            QueryPageDatasAsync().ContinueWith(_ => Reload?.Invoke());
        }
    }

    public int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize = value;
            QueryPageDatasAsync().ContinueWith(_ => Reload?.Invoke());
        }
    }

    public int _pageIndex = 1;
    public int PageIndex
    {
        get { return _pageIndex; }
        set
        {
            _pageIndex = value;
            QueryPageDatasAsync().ContinueWith(_ => Reload?.Invoke());
        }
    }

    public int PageCount { get; set; }

    public long TotalCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<RoleItemResponse>> Headers { get; set; }

     public bool IsOpenRoleForm { get; set; }

    public RolePage(AuthenticationCaller authenticationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        Headers = new()
        {
            new() { Text = i18n.T("Role.Name"), Value = nameof(RoleItemResponse.Name) },
            new() { Text = i18n.T("Role.Number"), Value = nameof(RoleItemResponse.Number) },
            new() { Text = i18n.T("State"), Value = nameof(RoleItemResponse.State) },
            new() { Text = i18n.T("CreationTime"), Value = nameof(RoleItemResponse.CreationTime), Sortable = false },
            new() { Text = i18n.T("Describe"), Value = nameof(RoleItemResponse.Describe), Sortable = false },
            new() { Text = i18n.T("Action"), Value = "Action", Sortable = false }
        };
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetRoleItemsAsync(PageIndex, PageSize, State, Search);
        if (result.Success)
        {
            var pageData = result.Data!;
            PageCount = (int)pageData.Count;
            TotalCount = pageData.TotalPages;
            Datas.Add(new RoleItemResponse
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Describe = "Test",
                Number = 100,
                CreationTime = DateTime.Now,
                State = MASA.Framework.Admin.Contracts.Base.Enum.State.Enable,
            });
        }        
        Lodding = false;
    }

    public void OpenObjectForm()
    {
        CurrentData = new();
        IsOpenRoleForm = true;
    }

    public async Task<bool> AddAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.AddRoleAsync(new AddRoleRequest()
        {
            Name = CurrentData.Name,
            Number = CurrentData.Number,
            Describe = CurrentData.Describe,
        });
        await CheckApiResult(result, I18n.T("Added Role successfully"), result.Message);
        Lodding = false;

        return result.Success;
    }

    public async Task<bool> UpdateAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.EditRoleAsync(new EditRoleRequest
        {
            RuleId = CurrentData.Id,
            Name = CurrentData.Name,
            Describe = CurrentData.Describe,
        });
        await CheckApiResult(result, I18n.T("Edit Role successfully"), result.Message);
        Lodding = false;

        return result.Success;
    }

    public void OpenDeleteRoleDialog(RoleItemResponse item)
    {
        CurrentData = item;
        OpenDeleteConfirmDialog(DeleteAsync);
    }

    public async Task DeleteAsync(bool confirm)
    {
        if(confirm)
        {
            Lodding = true;
            var result = await AuthenticationCaller.DeleteRoleAsync(new DeleteRoleRequest
            {
                RuleId = CurrentData.Id,
            });           
            await CheckApiResult(result, I18n.T("Delete role successfully"), result.Message);
            Lodding = false;
        }
    }

    //public async Task QueryAuthorizeItemsAsync()
    //{
    //    Lodding = true;
    //    var result = await AuthenticationCaller.GetAuthorizeItemsAsync(CurrentData.Id);
    //    Error = !result.Success;
    //    Message = result.Message;
    //    Lodding = false;
    //    AuthorizeDatas = result.Data ?? new();
    //}

    async Task CheckApiResult(ApiResultResponseBase result, string successMessage, string errorMessage)
    {
        if (result.Success is false) OpenErrorDialog(errorMessage);
        else
        {
            OpenSuccessMessage(successMessage);
            await QueryPageDatasAsync();
        }
    }
}


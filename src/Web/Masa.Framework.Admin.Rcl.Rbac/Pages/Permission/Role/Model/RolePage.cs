namespace Masa.Framework.Admin.Rcl.Rbac;

public class RolePage : ComponentPageBase
{
    public List<RoleItemResponse> Datas { get; set; } = new();

    public RoleItemResponse CurrentData { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

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

    public NavigationManager NavigationManager { get; set; }

    public RolePage(AuthenticationCaller authenticationCaller,NavigationManager navigationManager, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        NavigationManager = navigationManager;
        Headers = new()
        {
            new() { Text = i18n.T("Role.Name"), Value = nameof(RoleItemResponse.Name) },
            new() { Text = i18n.T("Role.Number"), Value = nameof(RoleItemResponse.Number) },
            new() { Text = i18n.T("CreationTime"), Value = nameof(RoleItemResponse.CreationTime), Sortable = false },
            new() { Text = i18n.T("Description"), Value = nameof(RoleItemResponse.Description), Sortable = false },
            new() { Text = i18n.T("Action"), Value = "Action", Sortable = false }
        };
    }

    public async Task QueryPageDatasAsync()
    {
        Loading = true;
        var result = await AuthenticationCaller.GetRoleItemsAsync(PageIndex, PageSize, true, Search);
        if (result.Success)
        {
            var pageData = result.Data!;
            PageCount = (int)pageData.TotalPages;
            TotalCount = pageData.Count;
            Datas = pageData.Items.ToList();
        }        
        Loading = false;
    }

    public void OpenObjectForm()
    {
        CurrentData = new();
        IsOpenRoleForm = true;
    }

    public async Task<bool> AddAsync()
    {
        Loading = true;
        var request = new AddRoleRequest(CurrentData.Name, CurrentData.Description, CurrentData.Number);
        var result = await AuthenticationCaller.AddRoleAsync(request);
        await CheckApiResult(result, I18n.T("Added Role successfully"), I18n.T(result.Message));
        Loading = false;

        return result.Success;
    }

    public async Task<bool> UpdateAsync()
    {
        Loading = true;
        var request = new EditRoleRequest(CurrentData.Id, CurrentData.Name, CurrentData.Description);
        var result = await AuthenticationCaller.EditRoleAsync(request);
        await CheckApiResult(result, I18n.T("Edit Role successfully"), result.Message);
        Loading = false;

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
            Loading = true;
            var result = await AuthenticationCaller.DeleteRoleAsync(new DeleteRoleRequest
            {
                RoleId = CurrentData.Id,
            });           
            await CheckApiResult(result, I18n.T("Delete role successfully"), result.Message);
            Loading = false;
        }
    }

    public void NavigateToRoleDetails(RoleItemResponse item)
    {
        CurrentData = item.Copy();
        NavigationManager.NavigateTo($"/roleDetails/{CurrentData.Id}");
    }

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


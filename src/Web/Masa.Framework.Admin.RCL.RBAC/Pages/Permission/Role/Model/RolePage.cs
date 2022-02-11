using MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

namespace Masa.Framework.Admin.RCL.RBAC;

public class RolePage
{
    public List<RoleItemResponse> Datas { get; set; }

    public RoleItemResponse CurrentData { get; set; } = new();

    public List<AuthorizeItemResponse> AuthorizeDatas { get; set; }

    public AuthorizeItemResponse CurrentAuthorizeData { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public int State { get; set; } = -1;

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public bool Lodding { get; set; }

    public bool Error { get; set; }

    public string? Message { get; set; }

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<RoleItemResponse>> Headers = new()
    {
        new() { Text = "NAME", Value = nameof(ObjectItemResponse.Name) },
        new() { Text = "CODE", Value = nameof(ObjectItemResponse.Code), Sortable = false },
        new() { Text = "STATE", Value = nameof(ObjectItemResponse.State) },
        new() { Text = "TYPE", Value = nameof(ObjectItemResponse.ObjectType), Sortable = false },
        new() { Text = "ACTIONS", Value = "Action", Sortable = false }
    };

    public bool IsAdd => CurrentData.Id != Guid.Empty;

    public RolePage(AuthenticationCaller authenticationCaller)
    {
        AuthenticationCaller = authenticationCaller;
        Datas = new();
        AuthorizeDatas=new();
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetRoleItemsAsync(PageIndex, PageSize, State, Search);
        Error = !result.Success;
        Message = result.Message;
        if (result.Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            Datas = pageData.Items.ToList();
        }
        Lodding = false;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        if (CurrentData.Id != Guid.Empty)
        {
            result = await AuthenticationCaller.AddRoleAsync(new AddRoleRequest()
            {
                Name = CurrentData.Name,
                Number = CurrentData.Number,
                Describe = CurrentData.Describe,
            });
        }
        else
        {
            result = await AuthenticationCaller.EditRoleAsync(new EditRoleRequest
            {
                RuleId = CurrentData.Id,
                Name = CurrentData.Name,
                Describe = CurrentData.Describe,
            });
        }
        Error = result.Success;
        Message = result.Message;
        Lodding = false;
    }

    public async Task DeleteAsync()
    {
        await Task.CompletedTask;
    }

    public async Task QueryAuthorizeItemsAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetAuthorizeItemsAsync(CurrentData.Id);
        Error = !result.Success;
        Message = result.Message;
        Lodding = false;
        AuthorizeDatas = result.Data ?? new();
    }
}


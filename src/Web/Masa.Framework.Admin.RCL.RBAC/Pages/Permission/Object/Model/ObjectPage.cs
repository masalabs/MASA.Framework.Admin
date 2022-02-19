using MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

namespace Masa.Framework.Admin.RCL.RBAC;

public class ObjectPage : ComponentPageBase
{
    public List<ObjectItemResponse> Datas { get; set; } = new();

    public IEnumerable<ObjectItemResponse> SelectDatas { get; set; }=new List<ObjectItemResponse>();

    public ObjectItemResponse CurrentData { get; set; } = new();

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

    public int PageIndex { get; set; } = 1;

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

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<ObjectItemResponse>> Headers { get; set; }

    public bool IsOpenObjectForm { get; set; }

    public int ObjectType { get; set; } = -1;

    public bool IsAdd => CurrentData.Id == Guid.Empty;

    public ObjectPage(AuthenticationCaller authenticationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        Headers = new()
        {
            new() { Text = i18n.T("Object.Name"), Value = nameof(ObjectItemResponse.Name) },
            new() { Text = i18n.T("Code"), Value = nameof(ObjectItemResponse.Code), Sortable = false },
            new() { Text = i18n.T("State"), Value = nameof(ObjectItemResponse.State) },
            new() { Text = i18n.T("Type"), Value = nameof(ObjectItemResponse.ObjectType), Sortable = false },
            new() { Text = i18n.T("Action"), Value = "Action", Sortable = false }
        }; 
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetObjectItemsAsync(PageIndex, PageSize, ObjectType, Search);
        if (result.Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            Datas = pageData.Items.ToList();
        }
        Lodding = false;
    }

    public void OpenObjectForm(ObjectItemResponse? item = null)
    {
        CurrentData = item ?? new();
        IsOpenObjectForm = true;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        if (IsAdd)
        {
            var request = new AddObjectRequest(CurrentData.Code, CurrentData.Name, CurrentData.ObjectType);
            result = await AuthenticationCaller.AddObjectAsync(request);

            await CheckApiResult(result, I18n.T("Added object successfully"), result.Message);
        }
        else
        {
            var request = new EditObjectRequest(CurrentData.Id, CurrentData.Name);
            result = await AuthenticationCaller.EditObjectAsync(request);

            await CheckApiResult(result, I18n.T("Edit object successfully"), result.Message);
        }
        Lodding = false;
    }

    public void OpenDeleteObjectDialog(ObjectItemResponse item)
    {
        CurrentData = item;
        OpenDeleteConfirmDialog(DeleteAsync);
    }

    public async Task DeleteAsync(bool confirm)
    {
        if (confirm)
        {
            Lodding = true;
            var request = new DeleteObjectRequest { ObjectId = CurrentData.Id };
            var result = await AuthenticationCaller.DeleteObjectAsync(request);
            await CheckApiResult(result, I18n.T("Delete object successfully"), result.Message);
            Lodding = false;
        }
    }

    public void OpenBatchDeleteObjectDialog()
    {
        OpenDeleteConfirmDialog(BatchDeleteAsync);
    }

    public async Task BatchDeleteAsync(bool confirm)
    {
        if (confirm)
        {
            Lodding = true;
            var request = new DeleteObjectRequest { ObjectId = CurrentData.Id };
            var result = await AuthenticationCaller.DeleteObjectAsync(request);
            await CheckApiResult(result, I18n.T("Delete object successfully"), result.Message);
            Lodding = false;
        }
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

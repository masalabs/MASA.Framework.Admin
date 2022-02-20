using MASA.Framework.Admin.Contracts.Authentication.Old.Request.Objects;
using MASA.Framework.Admin.Contracts.Authentication.Old.Response;

namespace Masa.Framework.Admin.RCL.RBAC;

public class ObjectPage : ComponentPageBase
{
    public List<ObjectItemResponse> Datas { get; set; } = new();

    public IEnumerable<ObjectItemResponse> SelectDatas { get; set; }=new List<ObjectItemResponse>();

    public ObjectItemResponse CurrentData { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public int ObjectType { get; set; } = -1;

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

    public bool Lodding { get; set; }

    public bool Error { get; set; }

    public string? Message { get; set; }

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<ObjectItemResponse>> Headers { get; set; }

    public ObjectPage(AuthenticationCaller authenticationCaller, GlobalConfig globalConfig, I18n i18n) : base(globalConfig, i18n)
    {
        AuthenticationCaller = authenticationCaller;
        Headers = new()
        {
            new() { Text = T("Object.Name"), Value = nameof(ObjectItemResponse.Name) },
            new() { Text = T("Code"), Value = nameof(ObjectItemResponse.Code), Sortable = false },
            new() { Text = T("State"), Value = nameof(ObjectItemResponse.State) },
            new() { Text = T("Type"), Value = nameof(ObjectItemResponse.ObjectType), Sortable = false },
            new() { Text = T("Action"), Value = "Action", Sortable = false }
        };

        string T(string key)
        {
            return i18n.T(key) ?? key;
        }
    }

    public async Task QueryPageDatasAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.GetObjectItemsAsync(PageIndex, PageSize, ObjectType, Search);
        Error = !result.Success;
        Message = result.Message;
        if (result.Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            Datas = pageData.Items.ToList();
            Datas.Add(new ObjectItemResponse()
            {
                Id = Guid.NewGuid(),
                Code = "9527",
                Name = "Test"
            });
        }
        Lodding = false;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        if (CurrentData.Id != Guid.Empty)
        {
            var input = new AddObjectRequest
            {
                Name = CurrentData.Name,
                Code = CurrentData.Code,
                ObjectType = CurrentData.ObjectType,
            };
            result = await AuthenticationCaller.AddObjectAsync(input);
        }
        else
        {
            var input = new EditObjectRequest
            {
                Name = CurrentData.Name,
                ObjectId = CurrentData.Id,
            };
            result = await AuthenticationCaller.EditObjectAsync(input);
        }
        Error = !result.Success;
        Message = result.Message;
        Lodding = false;
    }

    public async Task DeleteAsync()
    {
        Lodding = true;
        var result = await AuthenticationCaller.DeleteObjectAsync(new DeleteObjectRequest
        {
            ObjectId = CurrentData.Id,
        });
        Error = !result.Success;
        Message = result.Message;
        Lodding = false;
    }
}

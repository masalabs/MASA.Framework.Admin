using MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

namespace Masa.Framework.Admin.RCL.RBAC;

public class ObjectPage
{
    public List<ObjectItemResponse> ObjectDatas { get; set; }

    public ObjectItemResponse CurrentObjectData { get; set; } = new();

    private AuthenticationCaller AuthenticationCaller { get; set; }

    public int ObjectType { get; set; } = -1;

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public bool Lodding { get; set; }

    public bool Error { get; set; }

    public string? Message { get; set; }

    public long CurrentCount { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<ObjectItemResponse>> Headers = new()
    {
        new() { Text = "NAME", Value = nameof(ObjectItemResponse.Name) },
        new() { Text = "CODE", Value = nameof(ObjectItemResponse.Code), Sortable = false },
        new() { Text = "STATE", Value = nameof(ObjectItemResponse.State) },
        new() { Text = "TYPE", Value = nameof(ObjectItemResponse.ObjectType), Sortable = false },
        new() { Text = "ACTIONS", Value = "Action", Sortable = false }
    };

    public ObjectPage(AuthenticationCaller authenticationCaller)
    {
        AuthenticationCaller = authenticationCaller;
        ObjectDatas = new();
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
            ObjectDatas = pageData.Items.ToList();
        }
        Lodding = false;
    }

    public async Task AddOrUpdateAsync()
    {
        Lodding = true;
        var result = default(ApiResultResponseBase);
        if (CurrentObjectData.Id != Guid.Empty)
        {
            var input = new AddObjectRequest
            {
                Name = CurrentObjectData.Name,
                Code = CurrentObjectData.Code,
                ObjectType = CurrentObjectData.ObjectType,
            };
            result = await AuthenticationCaller.AddObjectAsync(input);
        }
        else
        {
            var input = new EditObjectRequest
            {
                Name = CurrentObjectData.Name,
                ObjectId = CurrentObjectData.Id,
            };
            result = await AuthenticationCaller.EditObjectAsync(input);
        }
        Error = result.Success;
        Message = result.Message;
        Lodding = false;
    }

    public async Task DeleteAsync()
    {
        await Task.CompletedTask;
    }
}

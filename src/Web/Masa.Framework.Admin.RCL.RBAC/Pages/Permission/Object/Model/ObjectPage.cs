using MASA.Framework.Admin.Contracts.Authentication.Response;

namespace Masa.Framework.Admin.RCL.RBAC;

public class ObjectPage
{
    public List<ObjectItemResponse> ObjectDatas { get; set; }

    private ConfigurationCaller ConfigurationCaller { get; set; }

    public string? Role { get; set; }

    public string? Plan { get; set; }

    public string? Status { get; set; }

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public bool Success { get; set; } = true;

    public string Message { get; set; } = "";

    public long CurrentCount { get; set; }//=> GetFilterDatas().Count();

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<DataTableHeader<MenuItemResponse>> Headers = new()
    {
        new() { Text = "NAME", Value = nameof(MenuItemResponse.Name) },
        new() { Text = "ICON", Value = nameof(MenuItemResponse.Icon), Sortable = false },
        new() { Text = "SORT", Value = nameof(MenuItemResponse.Sort) },
        new() { Text = "DESCRIPTION", Value = nameof(MenuItemResponse.Describe), Sortable = false },
        new() { Text = "STATUS", Value = nameof(MenuItemResponse.Disabled) },
        new() { Text = "ACTIONS", Value = "Action", Sortable = false }
    };

    public ObjectPage(ConfigurationCaller configurationCaller)
    {
        ConfigurationCaller = configurationCaller;
        MenuDatas = new();
    }

    public async Task QueryPageDatasAsync()
    {
        var result = await ConfigurationCaller.GetItemsAsync(PageIndex, PageSize);
        Success = result.Success;
        Message = result.Message;
        if(Success)
        {
            var pageData = result.Data!;
            CurrentCount = pageData.Count;
            MenuDatas = pageData.Items.ToList();
        }
    }

    public async Task AddOrUpdateAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DeleteAsync()
    {
        await Task.CompletedTask;
    }
}


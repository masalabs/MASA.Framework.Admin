namespace Masa.Framework.Admin.RCL.RBAC;

public class MenuPage
{
    public List<MenuItemResponse> MenuDatas { get; set; }

    public MenuItemResponse CurrentMenuData { get; set; } = new();

    private ConfigurationCaller ConfigurationCaller { get; set; }

    public string? Role { get; set; }

    public string? Plan { get; set; }

    public string? Status { get; set; }

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

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

    public MenuPage(ConfigurationCaller configurationCaller)
    {
        ConfigurationCaller = configurationCaller;
        MenuDatas = new();
    }

    public async Task QueryPageDatasAsync()
    {
        var pageData = await ConfigurationCaller.GetItemsAsync(PageIndex, PageSize);
        CurrentCount = pageData.Count;
        MenuDatas = pageData.Items.ToList();
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


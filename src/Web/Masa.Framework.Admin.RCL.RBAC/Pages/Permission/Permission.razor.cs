namespace Masa.Framework.Admin.RCL.RBAC.Pages.Menu;

public partial class List
{
    public bool _visible;
    public MenuPage _menuPage = default!;

    [Inject]
    public ConfigurationCaller ConfigurationCaller { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _menuPage = new(ConfigurationCaller);
        await _menuPage.QueryPageDatasAsync();
    }

    public List<MenuItemResponse> QueryPageDatas()
    {
        _menuPage.QueryPageDatasAsync().ContinueWith(_ => InvokeAsync(StateHasChanged));

        return _menuPage.MenuDatas;
    }

    public void OpenAddMenuPage()
    {
        _menuPage.CurrentMenuData = new();
        OpenEditMenuPage();
    }

    public void OpenEditMenuPage() => _visible = true;

    private async Task DeleteMenuAsync()
    {
        await _menuPage.DeleteAsync();
    }
}


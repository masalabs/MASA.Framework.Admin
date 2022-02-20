using MASA.Framework.Admin.Blog.Shared;

namespace MASA.Framework.Admin.Blog.Data.Base;

public class BlogFrontComponentBase : PageBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    public void NavigateTo(string uri, bool forceLoad = false)
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri + uri, forceLoad);
    }

    public void Redirect(string uri, bool forceLoad = false)
    {
        NavigationManager.NavigateTo(uri);
    }
}

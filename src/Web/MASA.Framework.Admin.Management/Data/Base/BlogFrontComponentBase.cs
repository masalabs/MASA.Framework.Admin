using MASA.Framework.Admin.Management.Shared;

namespace MASA.Framework.Admin.Management.Data.Base;

public class BlogFrontComponentBase : PageBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    public BlogFrontLayout App { get; set; }

    public void Message(string content, AlertTypes type = default, int timeout = 3000)
    {
        App.Message(content, type, timeout);
    }

    public void NavigateTo(string uri, bool forceLoad = false)
    {
        NavigationManager.NavigateTo(NavigationManager.BaseUri + uri, forceLoad);
    }

    public void Redirect(string uri, bool forceLoad = false)
    {
        NavigationManager.NavigateTo(uri);
    }
}

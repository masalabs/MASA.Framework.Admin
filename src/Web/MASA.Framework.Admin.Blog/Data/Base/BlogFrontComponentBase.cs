using MASA.Framework.Admin.Blog.Shared;

namespace MASA.Framework.Admin.Blog.Data.Base;

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

    public void Confirm(
        string title,
        string content,
        Func<Task> onOk,
        AlertTypes type = default,
        string icon = default,
        string iconColor = default,
        string okText = "确定",
        string cancelText = "取消",
        string okColor = "primary",
        string cancelColor = "default",
        System.Action onCancel = default)
    {
        App.Confirm(title, content, onOk, type, icon, iconColor, okText, cancelText, okColor, cancelColor, onCancel);
    }
}

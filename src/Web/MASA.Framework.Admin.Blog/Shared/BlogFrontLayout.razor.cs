using BlazorComponent.Web;
using MASA.Blazor.Presets;

namespace MASA.Framework.Admin.Blog.Shared;

public partial class BlogFrontLayout
{
    private Message.Model _message = new();

    public string SearchName { get; set; }

    public delegate Task SearchEventDelegate();


    public event SearchEventDelegate SearchEvent;

    public void Message(string content, AlertTypes type = AlertTypes.None, int timeout = 3000)
    {
        _message = new Message.Model
        {
            Visible = true,
            Content = content,
            Timeout = timeout,
            Type = type
        };

        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        GlobalConfig.OnPageModeChanged += base.StateHasChanged;
    }

    public void Dispose()
    {
        GlobalConfig.OnPageModeChanged -= base.StateHasChanged;
    }

    private async Task Search(KeyboardEventArgs args)
    {
        if (args.Code == "Enter")
        {
            SearchEvent.Invoke();
        }
    }

    #region Confirm

    private Confirm.Model _confirm = new();

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
        _confirm = new Confirm.Model
        {
            Visible = true,
            Title = title,
            Content = content,
            Type = type,
            Icon = icon,
            IconColor = iconColor,
            OkText = okText,
            CancelText = cancelText,
            OkColor = okColor,
            CancelColor = cancelColor,
            OnCancel = EventCallback.Factory.Create<MouseEventArgs>(this, () => onCancel?.Invoke()),
            OnOk = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
            {
                try
                {
                    await onOk.Invoke();
                }
                catch (Exception e)
                {
                    Message(e.Message, AlertTypes.Error);
                }
            })
        };

        StateHasChanged();
    }

    #endregion
}
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

    private async Task Search()
    {
        SearchEvent.Invoke();
    }
}
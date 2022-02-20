using MASA.Framework.Extensions.Tools;
using MASA.Framework.Extensions.Tools.Emails.Model;

namespace MASA.Framework.Admin.Blog.Shared;

public partial class BlogFrontLayout
{
    [Inject] private IPopupService PopupService { get; set; }

    public string SearchName { get; set; }

    public delegate Task SearchEventDelegate();

    public event SearchEventDelegate SearchEvent;

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

    #region email

    private DataModal<EmailParameter> _emailDataModal = new();
    private string _recipientArry;

    public void UserMessage()
    {
        _emailDataModal.Show();
    }

    public void SendEmail()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_recipientArry))
            {
                _ = PopupService.MessageAsync("请输入收件人");

            }

            if (string.IsNullOrWhiteSpace(_emailDataModal.Data.Title))
            {
                _ = PopupService.MessageAsync("请输入标题");
            }

            _emailDataModal.Data.RecipientArry = _recipientArry.Split(',');

            EmailService.Send(_emailDataModal.Data);

            _emailDataModal.Hide();

            _ = PopupService.MessageAsync("发送成功", AlertTypes.Success);

        }
        catch (Exception e)
        {
            _ = PopupService.MessageAsync(e);
        }
    }

    #endregion
}
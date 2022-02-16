namespace MASA.Framework.Admin.Web.Base.Shared;

public abstract class AdminCompontentBase : ComponentBase
{
    private I18n? _i18n;

    [Inject]
    public I18n I18n
    {
        get
        {
            return _i18n ?? throw new Exception("please Inject I18n!");
        }
        set
        {
            _i18n = value;
        }
    }

    public string T(string key) => I18n.T(key);

    public void RegisterPage(ComponentPageBase componentPage)
    {
        componentPage.Reload = () => InvokeAsync(StateHasChanged);
        componentPage.Component = this;
    }
}

public abstract class ComponentPageBase
{
    public ComponentBase? _component { get; set; }

    public ComponentBase Component
    {
        get => _component ?? throw new Exception("Please registerPage ComponentPageBase in AdminCompontentBase !");
        set => _component = value;
    }

    public Func<Task>? Reload { get; set; }

    public GlobalConfig GlobalConfig { get; }

    public I18n I18n { get; }

    public ComponentPageBase(GlobalConfig globalConfig, I18n i18n)
    {
        GlobalConfig = globalConfig;
        I18n = i18n;
    }

    public void OpenDeleteConfirmDialog(Func<bool, Task> confirmFunc)
    {
        EventCallback<bool> callback = EventCallback.Factory.Create(Component, confirmFunc);
        GlobalConfig.OpenConfirmDialog(I18n.T("Operation confirmation"), I18n.T("Are you sure you need to delete?"), callback);
    }

    public void OpenErrorDialog(string message)
    {
        GlobalConfig.OpenConfirmDialog(I18n.T("Error"), message, default);
    }

    public void OpenInformationMessage(string message)
    {
        GlobalConfig.OpenMessage(message, MessageType.Information);
    }

    public void OpenSuccessMessage(string message)
    {
        GlobalConfig.OpenMessage(message, MessageType.Success);
    }

    public void OpenWarningMessage(string message)
    {
        GlobalConfig.OpenMessage(message, MessageType.Warning);
    }

    public void OpenErrorMessage(string message)
    {
        GlobalConfig.OpenMessage(message, MessageType.Error);
    }
}


namespace Masa.Framework.Admin.RCL.RBAC;

public abstract class RBACCompontentBase : ComponentBase
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

    public string T(string key)
    {
        return I18n.T(key) ?? key;
    }

    public void RegisterPage(ComponentPage componentPage) => componentPage.Reload = ()=> InvokeAsync(StateHasChanged);
}

public abstract class ComponentPage
{
    public Func<Task>? Reload { get; set; }   
}


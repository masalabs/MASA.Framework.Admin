using MASA.Framework.Admin.Blog.Shared;

namespace MASA.Framework.Admin.Blog;

public abstract class ProCompontentBase : ComponentBase
{
    private I18n? _languageProvider;

    [Inject]
    public I18n LanguageProvider
    {
        get
        {
            return _languageProvider ?? throw new Exception("please Inject I18n!");
        }
        set
        {
            _languageProvider = value;
        }
    }

    [Inject]
    public IPopupService PopupService { get; set; }
    
    [CascadingParameter(Name = nameof(TimeZoneOffset))]
    protected TimeSpan TimeZoneOffset { get; set; }

    public string T(string key)
    {
        return LanguageProvider.T(key) ?? key;
    }
}


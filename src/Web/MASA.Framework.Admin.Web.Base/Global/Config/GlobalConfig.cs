namespace Masa.Framework.Admin.Web.Base.Global;

public class GlobalConfig
{
    #region Field

    private bool _isDark;
    private string? _pageMode;
    private bool _expandOnHover;
    private bool _navigationMini;
    private string? _favorite;
    private NavModel? _currentNav;
    private bool _lodding;
    private bool _confirm;
    private CookieStorage? _cookieStorage;

    #endregion

    #region Property

    public static string IsDarkCookieKey { get; set; } = "GlobalConfig_IsDark";

    public static string PageModeKey { get; set; } = "GlobalConfig_PageMode";

    public static string NavigationMiniCookieKey { get; set; } = "GlobalConfig_NavigationMini";

    public static string ExpandOnHoverCookieKey { get; set; } = "GlobalConfig_ExpandOnHover";

    public static string FavoriteCookieKey { get; set; } = "GlobalConfig_Favorite";

    public I18nConfig? I18nConfig { get; set; }

    public string? Language
    {
        get => I18nConfig?.Language;
        set
        {
            if (I18nConfig is not null)
            {
                I18nConfig.Language = value;
                OnLanguageChanged?.Invoke();
            }
        }
    }

    public bool IsDark
    {
        get => _isDark;
        set
        {
            _isDark = value;
            _cookieStorage?.SetItemAsync(IsDarkCookieKey, value);
        }
    }

    public string PageMode
    {
        get => _pageMode ?? PageModes.PageTab;
        set
        {
            _pageMode = value;
            _cookieStorage?.SetItemAsync(PageModeKey, value);
            OnPageModeChanged?.Invoke();
        }
    }

    public bool NavigationMini
    {
        get => _navigationMini;
        set
        {
            _navigationMini = value;
            _cookieStorage?.SetItemAsync(NavigationMiniCookieKey, value);
        }
    }

    public bool ExpandOnHover
    {
        get => _expandOnHover;
        set
        {
            _expandOnHover = value;
            _cookieStorage?.SetItemAsync(ExpandOnHoverCookieKey, value);
        }
    }

    public string? Favorite
    {
        get => _favorite;
        set
        {
            _favorite = value;
            _cookieStorage?.SetItemAsync(FavoriteCookieKey, value);
        }
    }

    public NavModel? CurrentNav
    {
        get => _currentNav;
        set
        {
            _currentNav = value;
            OnCurrentNavChanged?.Invoke();
        }
    }

    public List<AuthorizeItemResponse> Permissions { get; set; } = new ();

    public bool IsAdmin { get; set; }

    public bool Lodding
    {
        get => _lodding;
        set
        {
            if (_lodding != value)
            {
                _lodding = value;
                OnLoddingChanged?.Invoke(_lodding);
            }
        }
    }

    public void OpenConfirmDialog(string title, string message, EventCallback<bool> confirmFunc)
    {
        OnConfirmChanged?.Invoke(title, message, confirmFunc);
    }

    public void OpenMessage(string message, MessageType messageType, int timeOut = 2)
    {
        OnMessageChanged?.Invoke(message, messageType, timeOut);
    }

    #endregion

    public GlobalConfig(CookieStorage cookieStorage, I18nConfig i18nConfig, IHttpContextAccessor httpContextAccessor)
    {
        _cookieStorage = cookieStorage;
        I18nConfig = i18nConfig;
        if (httpContextAccessor.HttpContext is not null) Initialization(httpContextAccessor.HttpContext.Request.Cookies);
    }

    #region event

    public delegate void GlobalConfigChanged();
    public delegate void LoddingChanged(bool lodding);
    public delegate void MessageChanged(string message, MessageType messageType, int timeOut);
    public delegate void ConfirmChanged(string title, string message, EventCallback<bool> confirmFunc);

    public event GlobalConfigChanged? OnPageModeChanged;
    public event GlobalConfigChanged? OnCurrentNavChanged;
    public event GlobalConfigChanged? OnLanguageChanged;
    public event LoddingChanged? OnLoddingChanged;
    public event ConfirmChanged? OnConfirmChanged;
    public event MessageChanged? OnMessageChanged;

    #endregion

    #region Method

    public void Initialization(IRequestCookieCollection cookies)
    {
        _isDark = Convert.ToBoolean(cookies[IsDarkCookieKey]);
        _pageMode = cookies[PageModeKey];
        _navigationMini = Convert.ToBoolean(cookies[NavigationMiniCookieKey]);
        _expandOnHover = Convert.ToBoolean(cookies[ExpandOnHoverCookieKey]);
        _favorite = cookies[FavoriteCookieKey];
    }
    #endregion
}

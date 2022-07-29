using System.Globalization;

namespace Masa.Framework.Admin.Web.Base.Global;

public class GlobalConfig
{
    #region Field

    private bool _isDark;
    private string? _pageMode;
    private bool _expandOnHover;
    private bool _navigationMini;
    private string? _favorite;
    private bool _loading;
    private CookieStorage? _cookieStorage;

    #endregion

    #region Property

    public static string IsDarkCookieKey { get; set; } = "GlobalConfig_IsDark";

    public static string PageModeKey { get; set; } = "GlobalConfig_PageMode";

    public static string NavigationMiniCookieKey { get; set; } = "GlobalConfig_NavigationMini";

    public static string ExpandOnHoverCookieKey { get; set; } = "GlobalConfig_ExpandOnHover";

    public static string FavoriteCookieKey { get; set; } = "GlobalConfig_Favorite";

    public I18n? I18n { get; set; }

    public CultureInfo? Language
    {
        get => I18n?.Culture;
        set
        {
            if (I18n is not null)
            {
                I18n.SetCulture(value);
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

    public bool Loading
    {
        get => _loading;
        set
        {
            if (_loading != value)
            {
                _loading = value;
                OnLoadingChanged?.Invoke(_loading);
            }
        }
    }

    public void OpenConfirmDialog(string title, string message, EventCallback<bool> confirmFunc)
    {
        OnConfirmChanged?.Invoke(title, message, confirmFunc);
    }

    public void OpenMessage(string message, MessageTypes messageType, int timeOut = 2)
    {
        OnMessageChanged?.Invoke(message, messageType, timeOut);
    }

    #endregion

    public GlobalConfig(CookieStorage cookieStorage, I18n i18N, IHttpContextAccessor httpContextAccessor)
    {
        _cookieStorage = cookieStorage;
        I18n = i18N;
        if (httpContextAccessor.HttpContext is not null) Initialization(httpContextAccessor.HttpContext.Request.Cookies);
    }

    #region event

    public delegate void GlobalConfigChanged();
    public delegate void LoadingChanged(bool loading);
    public delegate void MessageChanged(string message, MessageTypes messageType, int timeOut);
    public delegate void ConfirmChanged(string title, string message, EventCallback<bool> confirmFunc);

    public event LoadingChanged? OnLoadingChanged;
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

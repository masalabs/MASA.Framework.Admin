namespace MASA.Framework.Sdks.Authentication.Internal;

public class Routing
{
    #region Authorize

    public static readonly string AuthorizeList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.AUTHORIZE_SERVICE);

    #region Permission

    public static readonly string PermissionList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.PERMISSION_SERVICE);

    public static readonly string PermissionDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.PERMISSION_SERVICE, "{id}");

    public static readonly string OperatePermission = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.PERMISSION_SERVICE);

    #endregion

    #region Role

    public static readonly string RoleList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.ROLE_SERVICE);

    public static readonly string RoleListByIds = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "ids");

    public static readonly string RoleDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "{id}");

    public static readonly string OperateRole = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.ROLE_SERVICE);

    public static readonly string RoleSelect = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "select");

    #endregion

    #region Object

    public static string ObjectList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.OBJECT_SERVICE);

    public static string ObjectAll = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "all");

    public static string ContainsObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "contains");

    public static string OperateObject = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.OBJECT_SERVICE);

    public static string BatchDeleteObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "batchDelete");

    #endregion

    #endregion

    #region User

    public static string UserList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.USER_SERVICE);

    public static string UserDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.USER_SERVICE, "{id}");

    /// <summary>
    /// User additions, deletions and modifications use the same url
    /// </summary>
    public static string OperateUser = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.USER_SERVICE);

    public static string UserLogin = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.USER_SERVICE, "login");

    public static string UserRole = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.USER_SERVICE, "role");

    public static string UserStatistic = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.USER_SERVICE, "statistic");

    #endregion

    #region Configuration

    #region Menu

    public static string MenuList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.MENU_SERVICE);

    public static string AllMenus = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.MENU_SERVICE, "allMenus");

    public static string AnyChild = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.MENU_SERVICE, "anyChild");

    public static string OperateMenu = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.MENU_SERVICE);

    #endregion

    #endregion

    #region   LogStatistics

    public static string OperateLog = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.LOG_SERVICE);

    public static string LogList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.LOG_SERVICE);

    public static string DayStatistics = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.STATISTIC_SERVICE, "day");

    public static string HourStatistics = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.STATISTIC_SERVICE, "hour");

    #endregion
}
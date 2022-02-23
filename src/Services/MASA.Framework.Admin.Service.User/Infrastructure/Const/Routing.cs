using UrlPre = MASA.Framework.Admin.Infrastructure.Configurations.Const.UrlRule;

namespace MASA.Framework.Admin.Service.User.Infrastructure.Const;

public class Routing
{
    public static string UserList = string.Format(UrlPre.DEFAULT_SERVICE_LIST, UrlRule.USER_SERVICE);

    public static string UserDetail = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "{id}");

    /// <summary>
    /// User additions, deletions and modifications use the same url
    /// </summary>
    public static string OperateUser = string.Format(UrlPre.DEFAULT_SERVICE, UrlRule.USER_SERVICE);

    public static string UserLogin = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "login");

    public static string UserRole = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "role");

    public static string UserStatistic = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "statistic");
}


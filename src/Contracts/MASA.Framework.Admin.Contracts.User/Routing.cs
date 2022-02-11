namespace MASA.Framework.Admin.Contracts.User;

public class Routing
{
    public static string UserList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.USER_SERVICE);

    public static string UserDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.USER_SERVICE, "{id}");

    /// <summary>
    /// User additions, deletions and modifications use the same url
    /// </summary>
    public static string OperateUser = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.USER_SERVICE);
}

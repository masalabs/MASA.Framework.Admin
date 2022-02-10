namespace MASA.Framework.Admin.Contracts.Authentication;

public class Routing
{
    public static string PermissionList = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.PERMISSION_SERVICE, "items");

    public static string ObjectList = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.OBJECT_SERVICE, "items");

    public static string ObjectEdit = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.OBJECT_SERVICE, "edit");
}

namespace MASA.Framework.Sdks.Authentication.Internal;

public class Routing
{
    #region Authorize

    public static readonly string AuthorizeList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.AUTHORIZE_SERVICE);

    #endregion

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

    public static string ObjectAll = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE,"all");

    public static string ContainsObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "contains");

    public static string OperateObject = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.OBJECT_SERVICE);

    public static string BatchDeleteObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "batchDelete");

    #endregion
}

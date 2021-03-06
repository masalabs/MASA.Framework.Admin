namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.Extensions;

public class Routing
{
    #region Authorize

    public static readonly string AuthorizeList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.AUTHORIZE_SERVICE);

    #endregion

    #region Permission

    public static readonly string PermissionList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.PERMISSION_SERVICE);

    public static readonly string PermissionDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.PERMISSION_SERVICE, "{id}");

    public static readonly string OperatePermission = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.PERMISSION_SERVICE);

    public static readonly string PermissionListByIds = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.PERMISSION_SERVICE, "ids");

    #endregion

    #region Role

    public static readonly string RoleList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.ROLE_SERVICE);

    public static readonly string RoleListByIds = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "ids");

    public static readonly string PermissionsByRoles = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "permissionsByRoles");

    public static readonly string RoleDetail = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "{id}");

    public static readonly string OperateRole = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.ROLE_SERVICE);

    public static readonly string AddChildRoles = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "addChildRoles");

    public static readonly string RoleSelect = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "select");

    public static readonly string AllRoleItem = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "allRoleItem");

    public static readonly string AddRolePermission = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "addRolePermission");

    public static readonly string DeleteRolePermission = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.ROLE_SERVICE, "deleteRolePermission");

    #endregion

    #region Object

    public static string ObjectList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.OBJECT_SERVICE);

    public static string ObjectAll = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "all");

    public static string ContainsObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "contains");

    public static string OperateObject = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.OBJECT_SERVICE);

    public static string BatchDeleteObject = string.Format(UrlRule.DEFAULT_SERVICE_BASE, Const.UrlRule.OBJECT_SERVICE, "batchDelete");

    #endregion

    #region IntegrationEvent

    public static readonly string RolePermission = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.INTEGRATION_EVENT_SERVICE);

    #endregion

}

namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Extensions;

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

    #region IntegrationEvent

    public static readonly string RolePermission = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.INTEGRATION_EVENT_SERVICE);

    #endregion

}

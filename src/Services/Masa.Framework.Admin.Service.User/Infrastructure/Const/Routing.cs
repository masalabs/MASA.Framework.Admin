using UrlPre = Masa.Framework.Admin.Infrastructure.Configurations.Const.UrlRule;

namespace Masa.Framework.Admin.Service.User.Infrastructure.Const;

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

    public static string UserRoles = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "userRoles");

    public static string AllUser = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "allUser");

    public static string UserWithDepartment = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "department");

    public static string UserListByRole = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "userListByRole");

    public static string UserGroup = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "group");

    public static string UserStatistic = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_SERVICE, "statistic");

    public static string UserGroupList = string.Format(UrlPre.DEFAULT_SERVICE_LIST, UrlRule.USER_GROUP_SERVICE);

    public static string OperateGroup = string.Format(UrlPre.DEFAULT_SERVICE, UrlRule.USER_GROUP_SERVICE);

    public static string GroupPermission = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_GROUP_SERVICE, "permission");

    public static string GroupByUser = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_GROUP_SERVICE, "byuser");

    public static string GroupUsers = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_GROUP_SERVICE, "users");

    public static string GroupPermissions = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_GROUP_SERVICE, "permissions");

    public static string UserGroupSelect = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_GROUP_SERVICE, "select");

    public static string OperateDepartment = string.Format(UrlPre.DEFAULT_SERVICE, UrlRule.USER_DEPARTMENT_SERVICE);

    public static string DepartmentList = string.Format(UrlPre.DEFAULT_SERVICE_LIST, UrlRule.USER_DEPARTMENT_SERVICE);

    public static string DepartmentUsers = string.Format(UrlPre.DEFAULT_SERVICE_BASE, UrlRule.USER_DEPARTMENT_SERVICE, "users");

    #region IntegrationEvent

    public static readonly string GroupPermissionNotify = string.Format(UrlPre.DEFAULT_SERVICE, UrlRule.INTEGRATION_EVENT_SERVICE);

    #endregion
}


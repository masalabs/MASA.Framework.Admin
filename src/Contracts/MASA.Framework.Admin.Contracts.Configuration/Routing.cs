namespace MASA.Framework.Admin.Contracts.Configuration;

public class Routing
{
    public static string MenuList = string.Format(UrlRule.DEFAULT_SERVICE_LIST, Const.UrlRule.MENU_SERVICE);

    public static string OperateMenu = string.Format(UrlRule.DEFAULT_SERVICE, Const.UrlRule.MENU_SERVICE);
}

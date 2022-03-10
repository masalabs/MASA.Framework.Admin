namespace Masa.Framework.Admin.Rcl.Rbac.Pages.User.ViewModel;

public class LoginRecord
{
    public DateTime LoginTime { get; set; }

    public string IP { get; set; } = "*.*.*.*";

    public bool Success { get; set; }

    public string Browser { get; set; } = "";

    public string Address { get; set; } = "";
}


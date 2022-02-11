namespace MASA.Framework.Admin.Contracts.User.Request;

public class UserCreateRequest
{
    public string Name { get; set; } = "";

    public string Account { get; set; } = "";

    public string Email { get; set; } = "";

    public string Pwd { get; set; } = "";
}

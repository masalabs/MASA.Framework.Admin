namespace Masa.Framework.Sdks.Authentication.Request.Users;

public class UserLoginRequest
{
    public string Account { get; set; } = default!;

    public string Password { get; set; } = default!;
}


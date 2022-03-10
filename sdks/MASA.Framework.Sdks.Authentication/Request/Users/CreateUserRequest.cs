using System.ComponentModel.DataAnnotations;

namespace Masa.Framework.Sdks.Authentication.Request.Users;

public class CreateUserRequest
{
    public string Name { get; set; } = "";

    public string Account { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";

    public string Pwd { get; set; } = "";
}


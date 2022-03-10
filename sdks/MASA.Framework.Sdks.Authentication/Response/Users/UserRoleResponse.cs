namespace Masa.Framework.Sdks.Authentication.Response.Users;

public class UserRoleResponse
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";
}


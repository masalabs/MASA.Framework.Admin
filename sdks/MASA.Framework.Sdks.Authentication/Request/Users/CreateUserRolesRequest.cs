namespace Masa.Framework.Sdks.Authentication.Request.Users;

public class CreateUserRolesRequest
{
    public Guid RoleId { get; set; }

    public List<Guid> UserIds { get; set; } = new();
}


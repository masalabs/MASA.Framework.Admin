namespace MASA.Framework.Sdks.Authentication.Request.Users;

public class CreateUserRoleRequest
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}


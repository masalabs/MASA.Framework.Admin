namespace MASA.Framework.Sdks.Authentication.Request.Users;

public class RemoveUserRoleRequest
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}

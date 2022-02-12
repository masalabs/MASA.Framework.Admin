namespace MASA.Framework.Admin.Contracts.User.Request;

public class CreateUserRoleRequest
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}


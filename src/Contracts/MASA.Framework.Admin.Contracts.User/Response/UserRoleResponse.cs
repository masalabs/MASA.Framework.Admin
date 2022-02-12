namespace MASA.Framework.Admin.Contracts.User.Response;

public class UserRoleResponse
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";
}


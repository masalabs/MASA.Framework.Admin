namespace MASA.Framework.Sdks.Authentication.Request.Authentication.Role;

public class EditRoleRequest
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }
}

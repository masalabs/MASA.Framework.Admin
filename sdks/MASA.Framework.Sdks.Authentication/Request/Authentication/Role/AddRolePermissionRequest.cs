namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class AddRolePermissionRequest
{
    public Guid Creator { get; set; }

    public Guid PermissionId { get; set; }

    public Guid RoleId { get; set; }

    public AddRolePermissionRequest(Guid permissionId, Guid roleId)
    {
        Creator = Guid.NewGuid();
        PermissionId = permissionId;
        RoleId = roleId;
    }
}

namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class DeleteRolePermissionRequest
{
    public Guid Creator { get; set; }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public DeleteRolePermissionRequest(Guid roleId, Guid permissionId)
    {
        Creator = Guid.NewGuid();
        RoleId = roleId;
        PermissionId = permissionId;
    }
}

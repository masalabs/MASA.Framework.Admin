namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public PermissionType PermissionType { get; private set; }

    public PermissionEffect PermissionEffect { get; private set; }

    public Guid RoleId { get; private set; }

    public Role Role { get; private set; }

    public RolePermission(Guid permissionsId, PermissionEffect permissionEffect)
    {
        PermissionsId = permissionsId;
        PermissionEffect = permissionEffect;
    }
}

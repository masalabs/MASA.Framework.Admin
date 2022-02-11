namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public PermissionEffect PermissionEffect { get; private set; }

    public Role Role { get; private set; }

    public RolePermission(Guid permissionsId, PermissionEffect permissionEffect)
    {
        PermissionsId = permissionsId;
        PermissionEffect = permissionEffect;
    }
}

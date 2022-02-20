namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public PermissionType PermissionType { get; private set; }

    public PermissionEffect PermissionEffect { get; private set; }

    public Role Role { get; private set; }


    public RolePermission()
    {

    }

    public RolePermission(Guid permissionsId, PermissionType permissionType, PermissionEffect permissionEffect) : this()
    {
        PermissionsId = permissionsId;
        PermissionType = permissionType;
        PermissionEffect = permissionEffect;
    }

    public void ChangeEffect(PermissionEffect permissionEffect)
    {
        PermissionEffect = permissionEffect;
    }
}

namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public Role Role { get; private set; }

    private RolePermission()
    {
    }

    public RolePermission(Guid permissionsId) : this()
    {
        PermissionsId = permissionsId;
    }
}

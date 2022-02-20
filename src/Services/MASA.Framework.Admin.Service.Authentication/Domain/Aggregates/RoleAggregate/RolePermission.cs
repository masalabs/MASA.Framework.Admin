namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public Guid RoleId { get; set; }

    public Role Role { get; private set; }

    public RolePermission()
    {
        Id = Guid.NewGuid();
    }

    public RolePermission(Guid permissionsId) : this()
    {
        PermissionsId = permissionsId;
    }
}

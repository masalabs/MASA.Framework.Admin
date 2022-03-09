namespace Masa.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RolePermission : Entity<Guid>
{
    public Guid PermissionsId { get; private set; }

    public Role Role { get; private set; } = null!;

    public RolePermission(Guid permissionsId)
    {
        PermissionsId = permissionsId;
    }
}

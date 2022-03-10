namespace Masa.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RoleItem : Entity<Guid>
{
    public Guid RoleId { get; set; }

    public Role Role { get; private set; } = default!;

    public RoleItem(Guid roleId)
    {
        RoleId = roleId;
    }
}

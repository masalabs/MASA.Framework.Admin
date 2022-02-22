namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RoleItem : Entity<Guid>
{
    public Guid RoleId { get; set; }

    public Role Role { get; private set; }

    private RoleItem()
    {
        Id = Guid.NewGuid();
    }

    public RoleItem(Guid roleId) : this()
    {
        RoleId = roleId;
    }
}

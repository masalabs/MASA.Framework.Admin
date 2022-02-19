namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RoleItem : Entity<Guid>
{
    public Guid ParentRoleId { get; private set; }

    public Guid RoleId { get; private set; }

    public Role Role { get; private set; }

    public RoleItem(Guid childrenRoleId)
    {
        RoleId = childrenRoleId;
    }
}

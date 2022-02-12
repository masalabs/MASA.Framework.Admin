namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

public class RoleItem : Entity<Guid>
{
    public Guid ChildrenRoleId { get; set; }

    public Role Role { get; private set; }

    public RoleItem(Guid childrenRoleId)
    {
        ChildrenRoleId = childrenRoleId;
    }
}

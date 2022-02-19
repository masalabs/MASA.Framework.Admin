namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class RoleItem : Entity<Guid>
{
    public Guid ParentRoleId { get; private set; }

    public Guid RoleId { get; set; }

    public Role Role { get; private set; }

    public RoleItem()
    {

    }

    public RoleItem(Guid roleId) : this()
    {
        RoleId = roleId;
    }
}

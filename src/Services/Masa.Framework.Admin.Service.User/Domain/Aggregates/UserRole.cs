namespace Masa.Framework.Admin.Service.User.Domain.Aggregates;

public class UserRole : Entity<Guid>
{
    public Guid RoleId { get; private set; }

    public virtual User User { get; set; } = default!;

    public UserRole(Guid roleId)
    {
        RoleId = roleId;
    }
}


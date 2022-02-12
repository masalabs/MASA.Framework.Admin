namespace MASA.Framework.Admin.Service.User.Domain.Aggregate;

public class UserRole : Entity<Guid>
{
    public Guid RoleId { get; private set; }

    public User User { get; private set; } = default!;

    public UserRole(Guid roleId)
    {
        RoleId = roleId;
    }
}


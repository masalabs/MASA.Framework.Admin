using MASA.Framework.Admin.Infrastructure.Utils;
using MASA.Framework.Admin.Service.Api.Infrastructure.Utils;

namespace MASA.Framework.Admin.Service.User.Domain.Aggregates;

public class User : AuditAggregateRoot<Guid, Guid>
{
    public string Account { get; private set; } = default!;

    public string? Name { get; set; }

    public string Password { get; private set; } = default!;

    public string Salt { get; private set; } = default!;

    public bool Gender { get; set; }

    public string? Cover { get; set; }

    public string? Email { get; set; }

    public State State { get; private set; }

    public DateTimeOffset LastLoginTime { get; private set; }

    public DateTimeOffset LastUpdateTime { get; private set; }

    private List<UserRole> userRoles;

    public IReadOnlyCollection<UserRole> UserRoles => userRoles;

    private User()
    {
        Id = Guid.NewGuid();
        State = State.Enable;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
        userRoles = new();
    }

    public User(Guid? creator, string account, string password) : this()
    {
        Account = account;
        Salt = RandomUtils.GenerateSpecifiedString(6);
        Password = SHA1Utils.Encrypt($"{password}{Salt}");
        Creator = creator ?? Id;
        Modifier = Creator;
        userRoles = new();
    }

    public void AddRole(Guid roleId)
    {
        if (!userRoles.Any(r => r.RoleId == roleId))
        {
            userRoles.Add(new UserRole(roleId));
        }
    }
}

namespace MASA.Framework.Admin.Service.User.Domain.Aggregate;

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
        State = State.Enable;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
        userRoles = new();
    }

    public User(Guid? creator, string account, string password) : this()
    {
        Account = account;
        Salt = "123456";
        Password = password;
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

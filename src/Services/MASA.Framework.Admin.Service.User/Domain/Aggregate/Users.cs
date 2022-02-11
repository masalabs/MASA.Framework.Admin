namespace MASA.Framework.Admin.Service.User.Domain.Aggregate;

public class Users : AuditAggregateRoot<Guid, Guid>
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

    private Users()
    {
        State = State.Enable;
        LastLoginTime = DateTimeOffset.Now;
        LastUpdateTime = DateTimeOffset.Now;
    }

    public Users(Guid? createUserId, string account, string password) : this()
    {
        Account = account;
        Salt = "123456";
        Password = password;
        Creator = createUserId ?? Id;
        Modifier = Creator;
    }
}

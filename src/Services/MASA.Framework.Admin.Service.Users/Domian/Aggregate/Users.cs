using MASA.Framework.Admin.Contracts.Users.Enum;

namespace MASA.Framework.Admin.Service.Users.Domian.Aggregate;

public class Users : AggregateRoot<Guid>
{
    public UserType Type { get; set; }

    public string Account { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public bool Gender { get; set; }

    public string Cover { get; set; }

    public string Email { get; set; }

    public State State { get; set; }

    public DateTimeOffset CreationTime { get; set; }

    public DateTimeOffset LastLoginTime { get; set; }

    public DateTimeOffset LastUpdateTime { get; set; }
}

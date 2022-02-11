namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

public class Role : AuditAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; } = default!;

    public string? Describe { get; set; }

    public int Number { get; private set; }

    public State State { get; private set; }

    private readonly List<RolePermission> permissions;

    public IReadOnlyCollection<RolePermission> Permissions => permissions;

    private Role() { }

    public Role(string name, int number = -1)
    {
        Name = name;
        Number = number;
        State = State.Enable;
    }

    public void Update(string name, string describe)
    {
        Name = name;
        Describe = describe;
    }
}

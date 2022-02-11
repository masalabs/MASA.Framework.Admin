namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

public class Role : AuditAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; } = default!;

    public string? Describe { get; set; }

    public int Number { get; private set; }

    public State State { get; private set; }

    private readonly List<RolePermission> permissions;

    public IReadOnlyCollection<RolePermission> Permissions => permissions;

    private readonly List<RoleItem> roleItems;

    public IReadOnlyCollection<RoleItem> RoleItems => roleItems;

    private Role()
    {
        permissions = new();
        roleItems = new();
    }

    public Role(Guid userId, string name, int number = -1) : this()
    {
        Creator = userId;
        Modifier = userId;
        Name = name;
        Number = number;
        State = State.Enable;
    }

    public void SetInheritedRole(List<Guid> ids)
    {
        this.roleItems.Clear();
        this.roleItems.AddRange(ids.Select(id => new RoleItem(id)));
    }

    public void Update(string name, string describe)
    {
        Name = name;
        Describe = describe;
    }
}

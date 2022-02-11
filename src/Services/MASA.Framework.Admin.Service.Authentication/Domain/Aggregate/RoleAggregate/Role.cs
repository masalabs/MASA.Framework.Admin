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

    public Role(Guid creator, string name, int number = -1) : this()
    {
        Creator = creator;
        Modifier = creator;
        Name = name;
        Number = number;
        State = State.Enable;
    }

    public void SetInheritedRole(List<Guid>? ids)
    {
        this.roleItems.Clear();

        if (ids != null)
            this.roleItems.AddRange(ids.Select(id => new RoleItem(id)));
    }

    public void Update(Guid creator, string name, string describe)
    {
        Modifier = creator;
        ModificationTime = DateTime.Now;
        Name = name;
        Describe = describe;
    }
}

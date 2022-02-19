namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class Role : AuditAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; } = default!;

    public string Describe { get; set; } = default!;

    public int Number { get; private set; }

    public bool Enable { get; private set; }

    private readonly List<RolePermission> permissions;

    public IReadOnlyCollection<RolePermission> Permissions => permissions;

    private readonly List<RoleItem> roleItems;

    public IReadOnlyCollection<RoleItem> RoleItems => roleItems;

    private Role()
    {
        Describe = string.Empty;
        permissions = new();
        roleItems = new();
    }

    public Role(Guid @operator, string name, int number = -1) : this()
    {
        Creator = @operator;
        Modifier = @operator;
        Name = name;
        Number = number;
        Enable = true;
    }

    public void SetInheritedRole(List<Guid>? roleIdList)
    {
        this.roleItems.Clear();

        if (roleIdList != null)
            this.roleItems.AddRange(roleIdList.Select(id => new RoleItem(id)));
    }

    public void Update(Guid @operator, string name, string? describe)
    {
        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
        Name = name;
        Describe = describe;
    }
}

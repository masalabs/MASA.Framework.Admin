namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class Role : AuditAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; } = default!;

    public string Describe { get; set; } = default!;

    public int Number { get; private set; }

    public bool Enable { get; private set; }

    private readonly List<RolePermission> permissions;

    public virtual IReadOnlyCollection<RolePermission> Permissions => permissions;

    private readonly List<RoleItem> roleItems;

    public virtual IReadOnlyCollection<RoleItem> RoleItems => roleItems;

    public Role()
    {
        Id = Guid.NewGuid();
        Describe = string.Empty;
        permissions = new List<RolePermission>();
        roleItems = new List<RoleItem>();
    }

    public Role(Guid @operator, string name, int number,string? describe) : this()
    {
        Creator = @operator;
        Modifier = @operator;
        Name = name;
        Number = number;
        Describe = describe ?? "";
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
        Describe = describe ?? string.Empty;
    }

    public void  AddRolePermission(Guid @operator, Guid permissionsId)
    {
        var permission = permissions.FirstOrDefault(permission => permission.PermissionsId == permissionsId);
        if (permission != null)
            throw new UserFriendlyException("This permission already exists for the current role, no need to grant it again");

        permissions.Add(new RolePermission(permissionsId));
        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
    }

    public void DeleteRolePermission(Guid @operator, Guid permissionsId)
    {
        var permission = permissions.FirstOrDefault(permission => permission.PermissionsId == permissionsId);
        if (permission == null)
            throw new UserFriendlyException(
                "Cancel authorization failed, please confirm that the current role has this permission and the current permission is not inherited");

        permissions.Remove(permission);

        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
    }
}

namespace Masa.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;

public class Role : AuditAggregateRoot<Guid, Guid>
{
    public string Name { get; private set; } = default!;

    public string Description { get; set; } = default!;

    public int Number { get; private set; }

    public bool Enable { get; private set; }

    private readonly List<RolePermission> permissions = new();

    public virtual IReadOnlyCollection<RolePermission> Permissions => permissions;

    private readonly List<RoleItem> roleItems = new();

    public virtual IReadOnlyCollection<RoleItem> RoleItems => roleItems;

    private Role()
    {

    }

    public Role(Guid @operator, string name, int number, string? description)
    {
        Creator = @operator;
        Modifier = @operator;
        Name = name;
        Number = number;
        Description = description ?? "";
        Enable = true;
    }

    public void SetInheritedRole(List<Guid>? roleIdList)
    {
        this.roleItems.Clear();
        if (roleIdList != null)
            this.roleItems.AddRange(roleIdList.Select(id => new RoleItem(id)));
    }

    public void Update(Guid @operator, string name, string? description)
    {
        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
        Name = name;
        Description = description ?? string.Empty;
    }

    public void AddRolePermission(Guid @operator, Guid permissionsId)
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

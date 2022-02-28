namespace Masa.Framework.Admin.Service.Authentication.Domain.Aggregates.PermissionAggregate;

public class Permission : AuditAggregateRoot<Guid, Guid>
{
    public ObjectType ObjectType { get; private set; }

    public string Name { get; private set; } = default!;

    public string Resource { get; private set; } = default!;

    public string Scope { get; private set; } = default!;

    public string Action { get; private set; } = default!;

    public string Code { get; private set; } = default!;

    public bool Enable { get; private set; }

    public PermissionType PermissionType { get; private set; }

    private Permission()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.UtcNow;
        ModificationTime = DateTime.UtcNow;
        Enable = true;
    }

    public Permission(
        Guid @operator,
        Guid id,
        ObjectType objectType,
        string name,
        string resource,
        string scope,
        string action,
        PermissionType permissionType) : this()
    {
        Creator = @operator;
        Id = id;
        CreationTime = DateTime.UtcNow;
        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
        ObjectType = objectType;
        Name = name;
        Resource = resource;
        Scope = scope;
        Action = action;
        Code = $"{Resource}/{scope}/{Action}";
        PermissionType = permissionType;
    }

    public void Update(Guid @operator, string name)
    {
        Modifier = @operator;
        ModificationTime = DateTime.UtcNow;
        Name = name;
    }

    public void ChangeState(Guid @operator, bool enable)
    {
        if (Enable != enable)
        {
            Modifier = @operator;
            ModificationTime = DateTime.UtcNow;
            Enable = enable;
        }
        else
        {
            throw new UserFriendlyException("The update is successful, no need to click repeatedly");
        }
    }
}

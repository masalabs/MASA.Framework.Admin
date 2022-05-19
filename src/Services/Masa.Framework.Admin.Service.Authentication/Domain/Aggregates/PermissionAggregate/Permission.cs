namespace Masa.Framework.Admin.Service.Authentication.Domain.Aggregates.PermissionAggregate;

public class Permission : FullAggregateRoot<Guid, Guid>
{

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

    public ObjectType ObjectType { get; }

    public string Name { get; private set; } = default!;

    public string Resource { get; } = default!;

    public string Scope { get; } = default!;

    public string Action { get; } = default!;

    public string Code { get; } = default!;

    public bool Enable { get; private set; }

    public PermissionType PermissionType { get; }

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

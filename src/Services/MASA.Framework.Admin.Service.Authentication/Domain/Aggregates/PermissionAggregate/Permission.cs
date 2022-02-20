namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.PermissionAggregate;

public class Permission : AuditAggregateRoot<Guid, Guid>
{
    public ObjectType ObjectType { get; private set; }

    public string Name { get; private set; }

    public string Resource { get; private set; }

    public string Scope { get; private set; }

    public string Action { get; private set; }

    public string Code { get; private set; }

    public bool Enable { get; private set; }

    public PermissionType PermissionType { get; private set; }

    private Permission()
    {
        CreationTime = DateTime.UtcNow;
        ModificationTime = DateTime.UtcNow;
        Enable = true;
    }

    public Permission(
        Guid @operator,
        ObjectType objectType,
        string name,
        string resource,
        string scope,
        string action,
        PermissionType permissionType) : this()
    {
        Creator = @operator;
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

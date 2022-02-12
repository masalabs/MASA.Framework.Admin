namespace MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.ObjectAggregate;

/// <summary>
/// Resources and Actions Constitute License
/// </summary>
public class ObjectItem : Entity<Guid>
{
    public string Name { get; private set; } = default!;

    public string Action { get; private set; } = default!;

    public string ObjectIdentifies { get; private set; }

    public PermissionType PermissionType { get; private set; }

    public Object Object{ get; private set; }

    public ObjectItem(string name, string objectIdentifies, string action, PermissionType permissionType)
    {
        Name = name;
        ObjectIdentifies = objectIdentifies;
        Action = action;
        PermissionType = permissionType;
    }

}

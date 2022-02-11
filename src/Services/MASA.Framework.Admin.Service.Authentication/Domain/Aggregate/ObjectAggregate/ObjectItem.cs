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

    public ObjectItem(string name, string action, string objectIdentifies, PermissionType permissionType)
    {
        Name = name;
        Action = action;
        ObjectIdentifies = objectIdentifies;
        PermissionType = permissionType;
    }

}

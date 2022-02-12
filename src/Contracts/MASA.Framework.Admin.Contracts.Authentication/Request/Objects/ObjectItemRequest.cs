namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class ObjectItemRequest
{
    public string Name { get; }

    public PermissionType PermissionType { get; }

    public string Action { get; }

    public string ObjectIdentifies { get; }

    public ObjectItemRequest(string name, PermissionType permissionType, string action, string objectIdentifies)
    {
        Name = name;
        PermissionType = permissionType;
        Action = action;
        ObjectIdentifies = objectIdentifies;
    }
}

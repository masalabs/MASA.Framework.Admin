namespace Masa.Framework.Admin.Service.Authentication.Response.Permission;

public class PermissionItemResponse
{
    public Guid Id { get; set; }

    public ObjectType ObjectType { get; set; }

    public string ObjectTypeName => ObjectType == ObjectType.Menu ? "menu" : "operator";

    public string Name { get; set; } = default!;

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    public string Action { get; set; } = default!;

    public bool Enable { get; set; }

    public PermissionType PermissionType { get; set; }

    public string PermissionTypeName => PermissionType == PermissionType.Private ? "Private" : "Public";

    public DateTime ModificationTime { get; set; }
}

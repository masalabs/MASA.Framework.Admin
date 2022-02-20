namespace MASA.Framework.Admin.Service.Authentication.Response.Permission;

public class PermissionDetailResponse
{
    public Guid Id { get; set; }

    public ObjectType ObjectType { get; set; }

    public string ObjectTypeName => ObjectType == ObjectType.Menu ? "menu" : "operator";

    public string Name { get; set; } = default!;

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    public string Action { get; set; } = default!;

    public PermissionType PermissionType { get; set; }

    public string PermissionTypeName => PermissionType == PermissionType.Private ? "Private" : "Public";
}

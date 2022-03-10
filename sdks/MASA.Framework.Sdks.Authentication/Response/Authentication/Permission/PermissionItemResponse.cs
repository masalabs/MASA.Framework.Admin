namespace Masa.Framework.Sdks.Authentication.Response.Authentication.Permission;

public class PermissionItemResponse
{
    public Guid Id { get; set; }

    public int ObjectType { get; set; }

    public string ObjectTypeName { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    public string Action { get; set; } = default!;

    public bool Enable { get; set; }

    public int PermissionType { get; set; }

    public string PermissionTypeName { get; set; } = default!;

    public DateTime ModificationTime { get; set; }
}

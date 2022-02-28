namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class AddPermissionRequest
{
    public Guid Creator { get; set; }

    public Guid PermissionId { get; set; }

    public ObjectType ObjectType { get; set; }

    public string Name { get; set; } = default!;

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    public string Action { get; set; } = default!;

    public Guid? RoleId { get; set; }

    public Guid? UserGroupId { get; set; }

    public PermissionType PermissionType { get; set; }

    public AddPermissionRequest()
    {
        PermissionId = Guid.NewGuid();
        Creator = Guid.NewGuid();
    }
}

public enum PermissionType
{
    Private = 1,
    Public
}

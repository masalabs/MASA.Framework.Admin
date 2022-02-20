namespace MASA.Framework.Admin.Service.Authentication.Response.Role;

public class AuthorizeItemResponse
{
    /// <summary>
    /// Primary key id of Role and Permission relationship table
    /// </summary>
    public Guid Id { get; set; }
    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = default!;

    public ObjectType ObjectType { get; set; }

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    /// <summary>
    /// The name of the role to which the license was granted
    /// </summary>
    public string InheritanceRoleSource { get; set; } = default!;

    public PermissionType PermissionType { get; set; }

    public PermissionEffect PermissionEffect { get; set; }
}

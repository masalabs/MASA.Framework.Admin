namespace Masa.Framework.Sdks.Authentication.Response.Authentication.Role;

public class AuthorizeItemResponse
{
    /// <summary>
    /// Primary key id of Role and Permission relationship table
    /// </summary>
    public Guid Id { get; set; }

    public Guid PermissionId { get; set; }

    [Required]
    public string PermissionName { get; set; } = default!;

    public ObjectType ObjectType { get; set; } = ObjectType.Menu;

    public string ObjectTypeName { get; set; } = default!;

    [Required]
    public string Resource { get; set; } = "menus";

    [Required]
    public string Scope { get; set; } = default!;

    public string ScopeName { get; set; } = default!;

    [Required]
    public string Action { get; set; } = default!;

    /// <summary>
    /// The name of the role to which the license was granted
    /// </summary>
    public string InheritanceRoleSource { get; set; } = default!;

    //public PermissionType PermissionType { get; set; }

    public PermissionType PermissionType { get; set; } = PermissionType.Public;
}

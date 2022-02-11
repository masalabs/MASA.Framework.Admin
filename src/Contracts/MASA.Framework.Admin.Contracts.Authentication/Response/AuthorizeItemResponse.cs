namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class AuthorizeItemResponse
{
    /// <summary>
    /// Primary key id of Role and Permission relationship table
    /// </summary>
    public Guid Id { get; set; }

    public string RoleName { get; set; } = default!;

    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = default!;

    public ObjectType ObjectType { get; set; }

    public string ObjectCode { get; set; } = default!;

    public string ObjectIdentifies { get; set; } = default!;

    public Guid? InheritanceRoleSource { get; set; }

    public string InheritanceSource { get; set; } = default!;

    public PermissionSource PermissionSource { get; set; }

    public PermissionType PermissionType { get; set; }

    public PermissionEffect PermissionEffect { get; set; }
}

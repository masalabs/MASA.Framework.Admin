using MASA.Framework.Admin.Contracts.Authentication.Old.Enum;

namespace MASA.Framework.Admin.Contracts.Authentication.Old.Request.Permissions;

public class CreatePermissionRequest
{
    public string Name { get; set; } = default!;

    public string Action { get; set; } = default!;

    public string ObjectId { get; set; } = default!;

    /// <summary>
    /// Unique resource identifier
    /// </summary>
    public string ObjectIdentifies { get; set; } = default!;

    public PermissionType PermissionType { get; set; }
}

using MASA.Framework.Admin.Contracts.Authentication.Old.Enum;

namespace MASA.Framework.Admin.Contracts.Authentication.Old.Response;

public class PermissionItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Action { get; set; } = default!;

    public ObjectType ObjectType { get; set; }

    public string ObjectCode { get; set; } = default!;

    public PermissionType PermissionType { get; set; }
}

using MASA.Framework.Admin.Contracts.Authentications.Enum;

namespace MASA.Framework.Admin.Contracts.Authentications.Response;

public class PermissionItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Action { get; set; }

    public ObjectType ObjectType { get; set; }

    public PermissionType Type { get; set; }
}

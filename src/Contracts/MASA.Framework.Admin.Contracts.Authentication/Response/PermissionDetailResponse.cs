namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class PermissionDetailResponse : PermissionItemResponse
{
    public string ObjectIdentifies { get; set; } = default!;
}

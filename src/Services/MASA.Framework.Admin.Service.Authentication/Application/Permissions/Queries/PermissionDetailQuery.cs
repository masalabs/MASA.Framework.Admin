namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public record PermissionDetailQuery(Guid PermissionId) : Query<PermissionDetailResponse>
{
    public override PermissionDetailResponse Result { get; set; } = default!;
}

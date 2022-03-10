namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public record IdListQuery(List<Guid> IdList) : Query<List<PermissionItemResponse>>
{
    public override List<PermissionItemResponse> Result { get; set; } = new();
}


namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record IdListQuery(List<Guid> IdList) : Query<List<RoleItemResponse>>
{
    public override List<RoleItemResponse> Result { get; set; } = new();
}



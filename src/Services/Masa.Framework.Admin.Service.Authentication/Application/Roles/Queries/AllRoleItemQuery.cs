namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record AllRoleItemQuery : Query<List<RoleItemsResponse>>
{
    public override List<RoleItemsResponse> Result { get; set; } = new();
}


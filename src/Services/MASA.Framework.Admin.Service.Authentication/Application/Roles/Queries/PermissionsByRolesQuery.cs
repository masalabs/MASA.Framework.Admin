namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record PermissionsByRolesQuery(List<Guid> RoleIds) : Query<List<AuthorizeItemResponse>>
{
    public override List<AuthorizeItemResponse> Result { get; set; } = new();
}

namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RolePermissionQuery(Guid RoleId) : Query<List<AuthorizeItemResponse>>
{
    public override List<AuthorizeItemResponse> Result { get; set; } = new();
}

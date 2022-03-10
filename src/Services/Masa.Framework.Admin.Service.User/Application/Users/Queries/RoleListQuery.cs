namespace Masa.Framework.Admin.Service.User.Application.Users.Queries;

public record RoleListQuery(Guid userId) : Query<List<UserRoleResponse>>
{
    public override List<UserRoleResponse> Result { get; set; } = new();
}


namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record RoleListQuery(Guid userId) : Query<List<UserRoleResponse>>
{
    public override List<UserRoleResponse> Result { get; set; } = new();
}


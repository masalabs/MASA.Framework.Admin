namespace MASA.Framework.Admin.Service.User.Application.Users.Queries;

public record UserListByRoleQuery(Guid roleId) : Query<List<UserItemResponse>>
{
    public override List<UserItemResponse> Result { get; set; } = new();
}



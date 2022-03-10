namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Queries;

public record GroupUserQuery(Guid GroupId) : Query<List<UserItemResponse>>
{
    public override List<UserItemResponse> Result { get; set; } = new();
}


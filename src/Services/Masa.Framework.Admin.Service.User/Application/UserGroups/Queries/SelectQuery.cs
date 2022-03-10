namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Queries;

public record SelectQuery : Query<List<UserGroupItemResponse>>
{
    public override List<UserGroupItemResponse> Result { get; set; } = null!;
}


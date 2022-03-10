namespace Masa.Framework.Admin.Service.User.Application.Users.Queries;

public record AllUserQuery : Query<List<UserItemResponse>>
{
    public override List<UserItemResponse> Result { get; set; } = new();
}


namespace Masa.Framework.Admin.Service.User.Application.Users.Queries;

public record DetailQuery(Guid UserId) : Query<UserDetailResponse>
{
    public override UserDetailResponse Result { get; set; } = default!;
}


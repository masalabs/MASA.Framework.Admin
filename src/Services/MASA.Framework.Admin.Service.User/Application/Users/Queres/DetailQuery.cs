namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record DetailQuery : Query<UserDetailResponse>
{
    public override UserDetailResponse Result { get; set; }
}


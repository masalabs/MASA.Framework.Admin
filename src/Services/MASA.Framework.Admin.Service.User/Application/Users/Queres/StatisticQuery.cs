namespace MASA.Framework.Admin.Service.User.Application.Users.Queres;

public record StatisticQuery : Query<UserStatisticResponse>
{
    public override UserStatisticResponse Result { get; set; } = new UserStatisticResponse();
}


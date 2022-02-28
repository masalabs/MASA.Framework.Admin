namespace Masa.Framework.Admin.Service.User.Application.Users.Queries;

public record StatisticQuery : Query<UserStatisticResponse>
{
    public override UserStatisticResponse Result { get; set; } = new UserStatisticResponse();
}


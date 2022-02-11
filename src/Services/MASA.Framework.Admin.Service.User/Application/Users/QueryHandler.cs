namespace MASA.Framework.Admin.Service.User.Application.Users;

public class QueryHandler
{
    readonly IUserRepository _userRepository;

    public QueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [EventHandler]
    public async Task HandlerAsync(DetailQuery detailQuery)
    {
        var user = await _userRepository.FindAsync(detailQuery.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");

        detailQuery.Result = new UserDetailResponse
        {
            Account = user.Account,
            Name = user.Name,
            Cover = user.Cover,
            Email = user.Email,
            Gender = user.Gender,
            LastLoginTime = user.LastLoginTime,
            LastUpdateTime = user.LastUpdateTime,
            CreationTime = user.CreationTime,
            State = (int)user.State
        };
    }
}


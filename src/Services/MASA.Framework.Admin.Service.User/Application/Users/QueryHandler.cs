namespace MASA.Framework.Admin.Service.User.Application.Users;

public class QueryHandler
{
    readonly IUserRepository _userRepository;

    public QueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [EventHandler]
    public async Task GetDetailAsync(UserQuery.DetailQuery detailQuery)
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

    [EventHandler]
    public async Task GetListAsync(UserQuery.ListQuery listQuery)
    {
        var users = await _userRepository.GetPaginatedListAsync((u) => u.Account.Contains(listQuery.Account), new PaginatedOptions
        {
            Page = listQuery.PageIndex,
            PageSize = listQuery.PageSize,
        });

        listQuery.Result = new PaginatedItemResponse<UserItemResponse>(
            listQuery.PageIndex,
            listQuery.PageSize,
            users.Total,
            users.TotalPages,
            users.Result.Select(user => new UserItemResponse()
            {
                Id = user.Id,
                Account = user.Account,
                Name = user.Name,
                Email = user.Email,
                State = (int)user.State,
                Cover = user.Cover,
                Gender = user.Gender,
                LastLoginTime = user.LastLoginTime
            }));
    }
}

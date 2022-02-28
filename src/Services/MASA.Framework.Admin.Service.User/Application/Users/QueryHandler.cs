using ListQuery = Masa.Framework.Admin.Service.User.Application.Users.Queries.ListQuery;

namespace Masa.Framework.Admin.Service.User.Application.Users;

public class QueryHandler
{
    readonly IUserRepository _userRepository;
    readonly IMemoryCache _memoryCache;
    readonly DbContext _dbContext;

    public QueryHandler(IUserRepository userRepository, UserDbContext dbContext, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    [EventHandler]
    public async Task GetDetailAsync(DetailQuery detailQuery)
    {
        var user = await _userRepository.FindAsync(detailQuery.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");

        detailQuery.Result = new UserDetailResponse
        {
            Id = user.Id,
            Account = user.Account,
            Name = user.Name,
            Cover = user.Cover,
            Email = user.Email,
            Gender = user.Gender,
            LastLoginTime = user.LastLoginTime,
            LastUpdateTime = user.LastUpdateTime,
            CreationTime = user.CreationTime,
            State = Convert.ToInt32(user.Enable)
        };
    }

    [EventHandler]
    public async Task GetListAsync(ListQuery listQuery)
    {
        var users = await _userRepository.GetPaginatedListAsync((u) => string.IsNullOrEmpty(listQuery.Account) || u.Account.Contains(listQuery.Account)
                    , new PaginatedOptions
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
                State = Convert.ToInt32(user.Enable),
                Cover = user.Cover,
                Gender = user.Gender,
                LastLoginTime = user.LastLoginTime
            }));
    }

    [EventHandler]
    public async Task GetAllUserAsync(AllUserQuery query)
    {
        var users = await _userRepository.GetListAsync();
        query.Result = users.Select(user => new UserItemResponse()
        {
            Id = user.Id,
            Account = user.Account,
            Name = user.Name,
            Email = user.Email,
            State = Convert.ToInt32(user.Enable),
            Cover = user.Cover,
            Gender = user.Gender,
            LastLoginTime = user.LastLoginTime
        }).ToList();
    }

    [EventHandler]
    public async Task GetUserListByRoleId(UserListByRoleQuery query)
    {
        query.Result =
            await (from userRole in _dbContext.Set<UserRole>().Include(ur => ur.User)
             where userRole.RoleId == query.roleId
             join user in _dbContext.Set<Domain.Aggregates.User>()
                 on userRole.User.Id equals user.Id
             select new UserItemResponse
             {
                 Id = user.Id,
                 Account = user.Account,
                 Name = user.Name,
                 Email = user.Email,
                 State = Convert.ToInt32(user.Enable),
                 Cover = user.Cover,
                 Gender = user.Gender,
                 LastLoginTime = user.LastLoginTime
             }).ToListAsync();
    }

    [EventHandler]
    public async Task GetUserRolesAsync(RoleListQuery roleListQuery)
    {
        var user = await _userRepository.GetByIdAsync(roleListQuery.userId);
        if (user is null)
        {
            throw new UserFriendlyException("userid not found");
        }

        roleListQuery.Result = user.UserRoles.Select(r => new UserRoleResponse
        {
            Id = r.Id,
            RoleId = r.RoleId
        }).ToList();
    }

    [EventHandler]
    public async Task GetUserCountAsync(StatisticQuery statisticQuery)
    {
        var userOnlineCount = _memoryCache.Get<List<OnlineUserModel>>("online_user_id")?.Count ?? 0;
        var userCount = await _userRepository.GetUserCountAsync((a)=>true);
        statisticQuery.Result = new UserStatisticResponse
        {
            UserCount = userCount,
            UserOnlineCount = userOnlineCount
        };
    }
}

using CreateCommand = Masa.Framework.Admin.Service.User.Application.Users.Commands.CreateCommand;

namespace Masa.Framework.Admin.Service.User.Application.Users;

public class CommandHandler
{
    readonly IUserRepository _userRepository;
    readonly LoginService _loginService;
    readonly IOptions<AppConfigOption> _options;
    readonly DbContext _dbContext;

    public CommandHandler(IUserRepository userRepository, UserDbContext dbContext, IMemoryCache memoryCache, IOptions<AppConfigOption> options)
    {
        _userRepository = userRepository;
        _loginService = new LoginService(memoryCache);
        _options = options;
        _dbContext = dbContext;
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteCommand deleteCommand)
    {
        var user = await _userRepository.FindAsync(deleteCommand.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");

        await _userRepository.RemoveAsync(user);
    }

    [EventHandler]
    public async Task CreateAsync(CreateCommand createCommand)
    {
        var user = new Domain.Aggregates.User(
            createCommand.Creator,
            createCommand.UserCreateRequest.Account,
            createCommand.UserCreateRequest.Pwd)
        {
            Name = createCommand.UserCreateRequest.Name,
            Email = createCommand.UserCreateRequest.Email,
            Gender = true,
        };
        await _userRepository.AddAsync(user);
    }

    [EventHandler]
    public async Task CreateUserRoleAsync(CreateRoleCommand createUserRoleCommand)
    {
        var user = await _userRepository.GetByIdAsync(createUserRoleCommand.UserRoleCreateRequest.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");
        user.AddRole(createUserRoleCommand.UserRoleCreateRequest.RoleId);
        await _userRepository.UpdateAsync(user);
    }

    [EventHandler]
    public async Task CreateUserRolesAsync(CreateUserRolesCommand command)
    {
        //var userRoles = command.UserIds.Select(id => new UserRole(command.RoleId) { User=new Domain.Aggregates.User(id) }).ToList();
        ////await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [user].user_roles WHERE role_id = CONVERT(uniqueidentifier,'{command.RoleId.ToString().ToUpper()}')");
        //var deleteUserRoles = await _dbContext.Set<UserRole>().Where(ur => command.UserIds.Contains(ur.User.Id)).ToListAsync();
        //_dbContext.Set<UserRole>().RemoveRange(deleteUserRoles);
        //await _dbContext.Set<UserRole>().AddRangeAsync(userRoles);
        string sql = $"DELETE FROM [user].user_roles WHERE role_id = '{command.RoleId}'";
        await _dbContext.Database.ExecuteSqlRawAsync(sql);

        foreach (var userId in command.UserIds)
        {
            await CreateUserRoleAsync(new CreateRoleCommand(new CreateUserRoleRequest()
            {
                UserId = userId,
                RoleId = command.RoleId
            }));
        }
    }

    [EventHandler]
    public async Task RemoveUserRoleAsync(RemoveRoleCommand removeUserRoleCommand)
    {
        var userRoleRequest = removeUserRoleCommand.RemoveRoleCreateRequest;
        var user = await _userRepository.GetByIdAsync(userRoleRequest.UserId);
        if (user == null)
        {
            throw new UserFriendlyException("userid not found");
        }
        user.RemoveRole(userRoleRequest.RoleId);
        await _userRepository.UpdateAsync(user);
    }

    [EventHandler]
    public async Task LoginAsync(LoginCommand loginCommand)
    {
        var user = await _userRepository.FindAsync(u => u.Account == loginCommand.UserLoginRequest.Account);
        if (user == null)
        {
            throw new UserFriendlyException("该账号不存在！");
        }

        if (!user.Enable)
        {
            throw new UserFriendlyException("该账号已禁用！");
        }

        string token = "";
        var pwd = SHA1Utils.Encrypt($"{loginCommand.UserLoginRequest.Password}{user.Salt}");
        if (pwd != user.Password)
        {
            throw new UserFriendlyException("密码错误！");
        }

        token = _loginService.GenerateJwtToken(user.Id, user.IsAdmin, _options.Value.Security, _options.Value.Expiration);
        loginCommand.Token = token;
    }

}

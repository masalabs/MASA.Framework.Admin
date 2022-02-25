using MASA.Framework.Admin.Service.User.Domain.Services;
using MASA.Utils.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using CreateCommand = MASA.Framework.Admin.Service.User.Application.Users.Commands.CreateCommand;

namespace MASA.Framework.Admin.Service.User.Application.Users;

public class CommandHandler
{
    readonly IUserRepository _userRepository;
    readonly LoginService _loginService;
    readonly IOptions<AppConfigOption> _options;

    public CommandHandler(IUserRepository userRepository, IMemoryCache memoryCache, IOptions<AppConfigOption> options)
    {
        _userRepository = userRepository;
        _loginService = new LoginService(memoryCache);
        _options = options;
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

        token = _loginService.GenerateJwtToken(user.Id, _options.Value.Security, _options.Value.Expiration);
        loginCommand.Token = token;
    }

}

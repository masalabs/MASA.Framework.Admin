namespace MASA.Framework.Admin.Service.User.Application.Users;

public class CommandHandler
{
    readonly IUserRepository _userRepository;

    public CommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        var user = await _userRepository.FindAsync(createUserRoleCommand.UserRoleCreateRequest.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");
        user.AddRole(createUserRoleCommand.UserRoleCreateRequest.RoleId);
        await _userRepository.UpdateAsync(user);
    }
}

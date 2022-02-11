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
        await _userRepository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task CreateAsync(CreateCommand createCommand)
    {
        var user = new Domain.Aggregate.Users(
            createCommand.LoginUserId,
            createCommand.UserCreateRequest.Account,
            createCommand.UserCreateRequest.Pwd)
        {
            Name = createCommand.UserCreateRequest.Name,
            Email = createCommand.UserCreateRequest.Email,
            Gender = true,
        };
        await _userRepository.AddAsync(user);
        await _userRepository.UnitOfWork.SaveChangesAsync();
    }
}

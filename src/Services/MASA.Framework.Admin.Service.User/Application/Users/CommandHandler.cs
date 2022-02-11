namespace MASA.Framework.Admin.Service.User.Application.Users;

public class CommandHandler
{
    readonly IUserRepository _userRepository;

    public CommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [EventHandler]
    public async Task HandlerAsync(DeleteCommand deleteCommand)
    {
        var user = await _userRepository.FindAsync(deleteCommand.UserId);
        if (user == null)
            throw new UserFriendlyException("userid not found");
        await _userRepository.RemoveAsync(user);
        await _userRepository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task HandlerAsync(CreateCommand createCommand)
    {
        await _userRepository.AddAsync(new Domain.Aggregate.Users
        {
            Account = createCommand.UserCreateRequest.Account,
            Name = createCommand.UserCreateRequest.Name,
            Email = createCommand.UserCreateRequest.Email,
            Password = createCommand.UserCreateRequest.Pwd
        });
        await _userRepository.UnitOfWork.SaveChangesAsync();
    }
}


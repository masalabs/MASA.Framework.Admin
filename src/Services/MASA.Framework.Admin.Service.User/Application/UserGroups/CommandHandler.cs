using CreateCommand = MASA.Framework.Admin.Service.User.Application.UserGroups.Commands.CreateCommand;

namespace MASA.Framework.Admin.Service.User.Application.UserGroups
{
    public class CommandHandler
    {
        readonly IUserGroupRepository _userGroupRepository;

        public CommandHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateCommand createCommand)
        {
            var userGroupRequest = createCommand.CreateUserGroupRequest;
            await _userGroupRepository.AddAsync(new UserGroup(userGroupRequest.Name, userGroupRequest.Code, userGroupRequest.Describtion));
        }

    }
}

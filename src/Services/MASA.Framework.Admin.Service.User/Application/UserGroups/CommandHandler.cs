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

        [EventHandler]
        public async Task CreateUserGroupAsync(CreateUserGroupCommand createUserGroupCommand)
        {
            var userGroupRequest = createUserGroupCommand.CreateUserGroupRequest;
            var userGroupItem = await _userGroupRepository.GetByIdAsync(userGroupRequest.UserGroupId);
            if (userGroupItem == null)
            {
                throw new UserFriendlyException("usergroupid not found");
            }
            userGroupItem.AddUser(userGroupRequest.UserId);
            await _userGroupRepository.UpdateAsync(userGroupItem);
        }

        [EventHandler]
        public async Task RemoveUserGroupAsync(RemoveUserRoleCommand removeUserGroupCommand)
        {
            var userGroupRequest = removeUserGroupCommand.RemoveUserGroupRequest;
            var userGroupItem = await _userGroupRepository.GetByIdAsync(userGroupRequest.UserGroupId);
            if (userGroupItem == null)
            {
                throw new UserFriendlyException("usergroupid not found");
            }
            userGroupItem.RemoveUser(userGroupRequest.UserId);
            await _userGroupRepository.UpdateAsync(userGroupItem);
        }
    }
}

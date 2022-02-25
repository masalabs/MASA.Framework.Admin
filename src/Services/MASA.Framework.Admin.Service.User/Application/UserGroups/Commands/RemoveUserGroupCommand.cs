namespace MASA.Framework.Admin.Service.User.Application.UserGroups.Commands
{
    public record RemoveUserRoleCommand(RemoveUserGroupRequest RemoveUserGroupRequest) : AdminCommand;
}

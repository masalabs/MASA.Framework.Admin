namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Commands;

public record CreateGroupPermissionCommand(Guid GroupId, Guid PermissionId) : AdminCommand;


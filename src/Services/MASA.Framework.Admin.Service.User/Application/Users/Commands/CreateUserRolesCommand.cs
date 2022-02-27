namespace MASA.Framework.Admin.Service.User.Application.Users.Commands;

public record CreateUserRolesCommand(Guid RoleId,List<Guid> UserIds ) : AdminCommand;


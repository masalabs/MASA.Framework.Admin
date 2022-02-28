namespace Masa.Framework.Admin.Service.User.Application.Users.Commands;

public record CreateRoleCommand(CreateUserRoleRequest UserRoleCreateRequest) : AdminCommand;


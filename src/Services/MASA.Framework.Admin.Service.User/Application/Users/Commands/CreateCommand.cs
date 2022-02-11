namespace MASA.Framework.Admin.Service.User.Application.Users.Commands;

public record CreateCommand(UserCreateRequest UserCreateRequest) : AdminCommand();


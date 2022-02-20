namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record RefreshRolePermissionCommand(Guid RoleId) : Command;

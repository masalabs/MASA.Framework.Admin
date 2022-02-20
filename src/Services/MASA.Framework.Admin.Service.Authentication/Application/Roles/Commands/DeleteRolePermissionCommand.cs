namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record DeleteRolePermissionCommand(Guid RoleId, Guid PermissionId) : CommandBase;

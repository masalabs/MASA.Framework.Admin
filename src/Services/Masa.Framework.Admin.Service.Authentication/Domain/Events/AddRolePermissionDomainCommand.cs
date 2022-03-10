namespace Masa.Framework.Admin.Service.Authentication.Domain.Events;

public record AddRolePermissionDomainCommand(
        Guid Creator,
        Guid PermissionId,
        Guid RoleId)
    : DomainCommand;

namespace MASA.Framework.Admin.Service.Authentication.Domain.Events;

public record AddRolePermissionDomainEvent(
        Guid RoleId,
        int ObjectType,
        string Name,
        string Resource,
        string Scope,
        string Action,
        Guid PermissionId,
        int PermissionType,
        int PermissionEffect)
    : AddRolePermissionIntegraionEvent(
        RoleId,
        ObjectType,
        Name,
        Resource,
        Scope,
        Action,
        PermissionId,
        PermissionType,
        PermissionEffect), IDomainEvent;

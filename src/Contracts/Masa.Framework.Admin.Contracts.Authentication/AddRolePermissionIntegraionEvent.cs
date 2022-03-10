namespace Masa.Framework.Admin.Contracts.Authentication;

public record AddRolePermissionIntegraionEvent(
    Guid RoleId,
    int ObjectType,
    string Name,
    string Resource,
    string Scope,
    string Action,
    Guid PermissionId,
    int PermissionType) : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(AddRolePermissionIntegraionEvent);
}

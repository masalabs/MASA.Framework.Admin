namespace Masa.Framework.Admin.Contracts.Authentication;

public record AddGroupPermissionIntegraionEvent(Guid GroupId, Guid PermissionId) : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(AddGroupPermissionIntegraionEvent);
}


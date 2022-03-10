namespace Masa.Framework.Admin.Contracts.Authentication;

public record AddRoleIntegraionEvent(
    Guid RoleId,
    string Name,
    string Describe,
    int Number,
    bool Enable,
    List<Guid> ChildrenRoleIds) : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(AddRoleIntegraionEvent);
}

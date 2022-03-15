namespace Masa.Framework.Admin.Service.Authentication.Domain.Events;

public record AddRoleDomainEvent(
    Guid RoleId,
    string Name,
    string Description,
    int Number,
    bool Enable,
    List<Guid> ChildrenRoleIds) :
    AddRoleIntegraionEvent(
        RoleId,
        Name,
        Description,
        Number,
        Enable,
        ChildrenRoleIds)
{

}

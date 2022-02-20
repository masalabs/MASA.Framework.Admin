namespace MASA.Framework.Admin.Service.Authentication.Domain.Events;

public record AddRoleDomainEvent(
    Guid RoleId,
    string Name,
    string Describe,
    int Number,
    bool Enable,
    List<Guid> ChildrenRoleIds) :
    AddRoleIntegraionEvent(
        RoleId,
        Name,
        Describe,
        Number,
        Enable,
        ChildrenRoleIds)
{

}

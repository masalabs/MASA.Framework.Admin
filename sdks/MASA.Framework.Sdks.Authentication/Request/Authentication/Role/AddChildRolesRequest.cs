namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class AddChildRolesRequest
{
    public Guid RoleId { get; set; }

    public List<Guid> ChildrenRoleIds { get; set; }

    public AddChildRolesRequest(Guid roleId, List<Guid> childrenRoleIds)
    {
        RoleId = roleId;
        ChildrenRoleIds = childrenRoleIds;
    }
}

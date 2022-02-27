namespace MASA.Framework.Admin.Service.Authentication.Response.Role;

public class RoleDetailResponse : RoleItemResponse
{
    public List<Guid> ChildrenRoleIds { get; set; } = new();

    public List<AuthorizeItemResponse> Permissions { get; set; } = new();
}

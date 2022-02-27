namespace MASA.Framework.Sdks.Authentication.Response.Authentication.Role;

public class RoleDetailResponse : RoleItemResponse
{
    public List<Guid> ChildrenRoleIds { get; set; } = new();

    public List<AuthorizeItemResponse> Permissions { get; set; } = new();
}

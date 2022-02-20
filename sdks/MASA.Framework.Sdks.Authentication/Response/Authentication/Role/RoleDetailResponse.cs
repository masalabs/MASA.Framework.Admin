namespace MASA.Framework.Sdks.Authentication.Response.Authentication.Role;

public class RoleDetailResponse : RoleItemResponse
{
    public List<AuthorizeItemResponse> Permissions { get; set; } = new();
}

namespace MASA.Framework.Admin.Service.Authentication.Response.Role;

public class RoleDetailResponse : RoleItemResponse
{
    public List<AuthorizeItemResponse> Permissions { get; set; } = new();
}

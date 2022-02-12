namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class RoleDetailResponse : RoleItemResponse
{
    public List<KeyValuePair<Guid, string>> ChildrenRoles { get; set; } = new();

    public List<AuthorizeItemResponse> Permissions { get; set; } = new();
}

namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class EditRoleRequest
{
    public Guid RoleId { get; set; }

    public string Name { get; set; }

    public string? Describe { get; set; }

    public EditRoleRequest(Guid roleId, string name, string? describe)
    {
        RoleId = roleId;
        Name = name;
        Describe = describe;
    }
}

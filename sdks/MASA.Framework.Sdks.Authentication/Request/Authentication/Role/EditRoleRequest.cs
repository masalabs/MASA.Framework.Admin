namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class EditRoleRequest
{
    public Guid RoleId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public EditRoleRequest(Guid roleId, string name, string? description)
    {
        RoleId = roleId;
        Name = name;
        Description = description;
    }
}

namespace Masa.Framework.Sdks.Authentication.Request.Authentication.Role;

public class AddRoleRequest
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public int Number { get; set; } = -1;

    public List<Guid> ChildrenRoleIds { get; set; }

    public AddRoleRequest(string name, string? description, int number, List<Guid>? childrenRoleIds = null)
    {
        Name = name;
        Description = description;
        Number = number;
        ChildrenRoleIds = childrenRoleIds ?? new();
    }
}

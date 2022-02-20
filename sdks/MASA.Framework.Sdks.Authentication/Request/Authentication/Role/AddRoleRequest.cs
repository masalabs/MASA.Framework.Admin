namespace MASA.Framework.Sdks.Authentication.Request.Authentication.Role;

public class AddRoleRequest
{
    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public int Number { get; set; } = -1;

    public List<Guid> ChildrenRoleIds { get; set; } = new();
}

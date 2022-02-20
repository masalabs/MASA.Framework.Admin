namespace MASA.Framework.Admin.Contracts.Authentication.Old.Request.Roles;

public class AddRoleRequest
{
    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public int Number { get; set; } = -1;

    public List<Guid>? ChildrenIds { get; set; } = null;
}

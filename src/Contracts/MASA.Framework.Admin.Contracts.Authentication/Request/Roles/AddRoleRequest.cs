namespace MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

public record AddRoleRequest
{
    public string Name { get; set; } = default!;

    public string Describe { get; set; } = default!;

    public int Number { get; set; }

    public List<Guid> ChildrenIds { get; set; } = default!;
}

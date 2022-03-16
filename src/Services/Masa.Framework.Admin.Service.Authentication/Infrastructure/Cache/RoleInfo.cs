namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.Cache;

public class RoleInfo
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int Number { get; set; }

    public bool Enable { get; set; }

    public List<Guid> ChildrenRoleIds { get; set; } = new();
}

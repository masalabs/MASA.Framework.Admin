namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Cache;

public class RoleInfo
{
    public string Name { get; set; }

    public string Describe { get; set; }

    public int Number { get; set; }

    public bool Enable { get; set; }

    public List<Guid> ChildrenRoleIds { get; set; }
}

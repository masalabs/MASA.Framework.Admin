namespace MASA.Framework.Admin.Service.Authentication.Response.Role;

public class RoleItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    /// <summary>
    /// Current role limit
    /// Unlimited: -1
    /// </summary>
    public int Number { get; set; }

    public bool Enable { get; set; }

    public DateTime CreationTime { get; set; }
}

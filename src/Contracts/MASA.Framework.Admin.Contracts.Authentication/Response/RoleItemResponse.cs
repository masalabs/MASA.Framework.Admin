namespace MASA.Framework.Admin.Contracts.Authentication.Response;

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

    public State State { get; set; }

    public DateTimeOffset CreationTime { get; set; }
}

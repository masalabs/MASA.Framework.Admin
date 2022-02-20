namespace MASA.Framework.Admin.Contracts.Authentication.Response;

public class RoleItemResponse
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    /// <summary>
    /// Current role limit
    /// Unlimited: -1
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Number { get; set; }

    public State State { get; set; } = State.Enable;

    public DateTimeOffset CreationTime { get; set; }

    public RoleItemResponse Copy() => (RoleItemResponse)MemberwiseClone();
}

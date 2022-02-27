namespace MASA.Framework.Sdks.Authentication.Response.Authentication.Role;

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

    public bool Enable { get; set; }

    public DateTime CreationTime { get; set; }

    public bool Select { get; set; }

    public RoleItemResponse Copy() => (RoleItemResponse)MemberwiseClone();
}

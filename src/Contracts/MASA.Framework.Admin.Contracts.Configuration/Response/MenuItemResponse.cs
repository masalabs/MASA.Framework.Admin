namespace MASA.Framework.Admin.Contracts.Configuration.Response;

public class MenuItemResponse
{
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    [Required]
    public string Url { get; set; } = default!;

    public Guid? ParentId { get; set; }

    public string? ParentName { get; set; }

    [Range(1,int.MaxValue)]
    public int Sort { get; set; }

    public bool Disabled { get; set; }

    public DateTimeOffset CreationTime { get; set; }
}

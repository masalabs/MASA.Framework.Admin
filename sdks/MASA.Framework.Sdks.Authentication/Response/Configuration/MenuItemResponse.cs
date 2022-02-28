namespace Masa.Framework.Sdks.Authentication.Response.Configuration;

public class MenuItemResponse
{
    public Guid Id { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public string Url { get; set; } = default!;

    public Guid? ParentId { get; set; }

    [Range(1, int.MaxValue)]
    public int Sort { get; set; }

    public bool Disabled { get; set; }

    public DateTimeOffset CreationTime { get; set; }

    public bool Select { get; set; }

    public MenuItemResponse Copy() => (MenuItemResponse)MemberwiseClone();
}

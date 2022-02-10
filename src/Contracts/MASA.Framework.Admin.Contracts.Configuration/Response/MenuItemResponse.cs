namespace MASA.Framework.Admin.Contracts.Configuration.Response;

public class MenuItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Describe { get; set; } = default!;

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public Guid? ParentId { get; set; }

    public string? ParentName { get; set; }

    public int Sort { get; set; }

    public DateTimeOffset CreationTime { get; set; }
}

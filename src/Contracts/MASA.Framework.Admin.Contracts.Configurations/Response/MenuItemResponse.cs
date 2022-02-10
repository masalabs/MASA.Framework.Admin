namespace MASA.Framework.Admin.Contracts.Configurations.Response;

public class MenuItemResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public Guid? ParentId { get; set; }

    public DateTimeOffset CreationTime { get; set; }
}

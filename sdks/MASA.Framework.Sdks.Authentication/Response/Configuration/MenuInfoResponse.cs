namespace Masa.Framework.Sdks.Authentication.Response.Configuration;

public class MenuInfoResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string Url { get; set; } = default!;

    public Guid? ParentId { get; set; }

    public string? ParentName { get; set; }

    public int Sort { get; set; }
}

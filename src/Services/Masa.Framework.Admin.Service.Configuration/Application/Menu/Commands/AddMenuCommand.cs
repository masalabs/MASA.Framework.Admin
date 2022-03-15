namespace Masa.Framework.Admin.Configuration.Application.Menu.Commands;

public record AddMenuCommand : CommandBase
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; } = default!;

    public int Sort { get; set; }

    public bool Enabled { get; set; } = true;

    public bool OnlyJump { get; set; }
}

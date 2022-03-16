namespace Masa.Framework.Admin.Configuration.Application.Menu.Commands;

public record EditMenuCommand : CommandBase
{
    public Guid MenuId { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; } = "";

    public int Sort { get; set; }

    public bool Enabled { get; set; } = true;

    public bool OnlyJump { get; set; }
}

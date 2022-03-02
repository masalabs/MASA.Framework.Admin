namespace Masa.Framework.Admin.Configuration.Application.Menu.Commands;

public record EditMenuCommand : CommandBase
{
    public Guid MenuId { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public string Url { get; set; }

    public int Sort { get; set; }

    public bool Disabled { get; set; }

    public bool OnlyJump { get; set; }
}

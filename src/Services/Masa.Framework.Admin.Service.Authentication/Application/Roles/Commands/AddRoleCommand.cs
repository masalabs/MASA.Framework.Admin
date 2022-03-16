namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record AddRoleCommand : CommandBase
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public int Number { get; set; } = -1;

    public List<Guid> ChildrenRoleIds { get; set; } = new();
}

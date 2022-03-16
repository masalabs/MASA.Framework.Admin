namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record EditRoleCommand : CommandBase
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public EditRoleCommand(Guid roleId, string name, string? description)
    {
        RoleId = roleId;
        Name = name;
        Description = description;
    }
}

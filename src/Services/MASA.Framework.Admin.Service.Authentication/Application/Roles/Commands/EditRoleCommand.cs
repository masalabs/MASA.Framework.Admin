namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record EditRoleCommand : CommandBase
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }

    public EditRoleCommand(Guid roleId, string name, string? describe)
    {
        RoleId = roleId;
        Name = name;
        Describe = describe;
    }
}

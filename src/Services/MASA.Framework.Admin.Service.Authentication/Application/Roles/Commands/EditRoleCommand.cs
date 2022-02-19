namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record EditRoleCommand : CommandBase
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }
}

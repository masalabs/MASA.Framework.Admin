namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public record EditPermissionCommand : CommandBase
{
    public Guid PermissionId { get; set; }

    public string Name { get; set; } = default!;
}

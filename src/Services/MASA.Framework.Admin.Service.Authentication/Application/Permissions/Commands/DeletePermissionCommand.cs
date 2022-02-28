namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public record DeletePermissionCommand : CommandBase
{
    public Guid PermissionId { get; set; }
}

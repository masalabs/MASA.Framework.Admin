namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public record ChangeStateCommand : CommandBase
{
    public Guid PermissionId { get; set; }

    public bool Enable { get; set; }
}

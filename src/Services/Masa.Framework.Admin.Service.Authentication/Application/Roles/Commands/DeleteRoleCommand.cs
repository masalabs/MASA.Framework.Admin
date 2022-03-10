namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record DeleteRoleCommand : CommandBase
{
    public Guid RoleId { get; set; }
}

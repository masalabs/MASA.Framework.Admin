namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record EditRoleCommand(EditRoleRequest Request) : AdminCommand
{
    public EditRoleRequest Request { get; set; } = Request;
}

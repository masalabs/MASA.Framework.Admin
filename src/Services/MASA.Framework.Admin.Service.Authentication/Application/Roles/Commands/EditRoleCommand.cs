namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record EditRoleCommand(EditRoleRequest Request) : Command
{
    public EditRoleRequest Request { get; set; } = Request;
}

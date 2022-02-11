namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record AddRoleCommand(AddRoleRequest Request) : Command
{
    public AddRoleRequest Request { get; } = Request;
}

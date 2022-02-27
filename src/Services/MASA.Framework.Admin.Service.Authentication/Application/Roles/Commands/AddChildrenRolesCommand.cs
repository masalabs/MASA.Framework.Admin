namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public record AddChildrenRolesCommand : CommandBase
{
    public Guid RoleId { get; set; }

    public List<Guid> ChildrenRoleIds { get; set; } = new();
}

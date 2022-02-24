namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public record AddPermissionCommand : CommandBase
{
    public ObjectType ObjectType { get; set; }

    public string Name { get; set; } = default!;

    public string Resource { get; set; } = default!;

    public string Scope { get; set; } = default!;

    public string Action { get; set; } = default!;

    public Guid? RoleId { get; set; }

    public PermissionType PermissionType { get; set; }
}
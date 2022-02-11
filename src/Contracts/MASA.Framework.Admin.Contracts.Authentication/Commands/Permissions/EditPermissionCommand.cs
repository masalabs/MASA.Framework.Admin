namespace MASA.Framework.Admin.Contracts.Authentication.Commands.Permissions;

public class EditPermissionCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Action { get; set; } = default!;

    /// <summary>
    /// Unique resource identifier
    /// </summary>
    public string ObjectIdentifies { get; set; } = default!;
}

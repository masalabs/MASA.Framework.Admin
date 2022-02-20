namespace MASA.Framework.Admin.Contracts.Authentication.Old.Request.Roles;

public class EditRoleRequest
{
    public Guid RuleId { get; set; }

    public string Name { get; set; } = default!;

    public string? Describe { get; set; }
}

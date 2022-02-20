namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RoleCacheDetailQuery(Guid RoleId) : Query<RoleInfo>
{
    public override RoleInfo Result { get; set; } = default!;
}

namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries.Cache;

public record RoleCacheDetailQuery(Guid RoleId) : Query<RoleInfo>
{
    public override RoleInfo Result { get; set; } = default!;
}

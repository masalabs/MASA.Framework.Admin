namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RoleBaseQuery(Guid RoleId) : Query<RoleInfo>
{
    public override RoleInfo Result { get; set; }
}

namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RoleDetailQuery(Guid RoleId) : DomainQuery<RoleDetailResponse>
{
    public override RoleDetailResponse Result { get; set; } = default!;
}

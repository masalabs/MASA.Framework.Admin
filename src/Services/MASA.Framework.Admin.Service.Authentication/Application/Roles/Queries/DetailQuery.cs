namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record DetailQuery(Guid RoleId) : DomainQuery<RoleDetailResponse>
{
    public override RoleDetailResponse Result { get; set; }
}

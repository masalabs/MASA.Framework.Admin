namespace Masa.Framework.Admin.Service.User.Application.UserGroups.Queries;

public record PermissionIdsQuery(Guid GroupId) : Query<List<Guid>>
{
    public override List<Guid> Result { get; set; } = new();
}


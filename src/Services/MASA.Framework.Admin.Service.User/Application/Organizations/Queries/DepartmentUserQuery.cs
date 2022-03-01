namespace Masa.Framework.Admin.Service.User.Application.Organizations.Queries;

public record DepartmentUserQuery(Guid DepartmentId, bool All) : Query<List<UserItemResponse>>
{
    public override List<UserItemResponse> Result { get; set; } = null!;
}


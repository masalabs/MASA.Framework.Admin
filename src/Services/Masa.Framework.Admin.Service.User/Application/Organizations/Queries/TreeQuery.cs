namespace Masa.Framework.Admin.Service.User.Application.Organizations.Queries;

public record TreeQuery(Guid ParentId) : Query<List<DepartmentItemResponse>>
{
    public override List<DepartmentItemResponse> Result { get; set; } = new();
}


namespace MASA.Framework.Admin.Service.User.Application.Organizations.Queries;

public record DepartmentUserQuery(int PageIndex, int PageSize, Guid DepartmentId) : Query<PaginatedItemResponse<UserItemResponse>>
{
    public override PaginatedItemResponse<UserItemResponse> Result { get; set; } = null!;
}


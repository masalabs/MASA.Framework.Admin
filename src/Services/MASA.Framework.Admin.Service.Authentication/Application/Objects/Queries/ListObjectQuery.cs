namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Queries;

public record ListObjectQuery(int PageIndex, int PageSize, int Type, string Name) : Query<PaginatedItemResponse<ObjectItemResponse>>
{
    public override PaginatedItemResponse<ObjectItemResponse> Result { get; set; } = default!;
}

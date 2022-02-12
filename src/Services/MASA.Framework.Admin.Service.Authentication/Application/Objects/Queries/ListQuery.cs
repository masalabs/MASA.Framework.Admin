namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Queries;

public record ListQuery(int PageIndex, int PageSize, int Type, string Name)
    : QueryItems<PaginatedItemResponse<ObjectItemResponse>>(PageIndex, PageSize)
{
    public override PaginatedItemResponse<ObjectItemResponse> Result { get; set; }
}

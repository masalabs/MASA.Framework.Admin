namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record ListQuery(int PageIndex, int PageSize, string Name, int State)
    : QueryItems<PaginatedItemResponse<RoleItemResponse>>(PageIndex, PageSize)
{
    public override PaginatedItemResponse<RoleItemResponse> Result { get; set; }
}

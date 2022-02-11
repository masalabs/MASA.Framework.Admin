namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RolesQuery(int PageIndex, int PageSize, string Name, int State)
    : QueryItems<PaginatedItemResponse<RoleItemResponse>>(PageIndex, PageSize)
{
    public int State { get; set; } = State;

    public string Name { get; set; } = Name;

    public override PaginatedItemResponse<RoleItemResponse> Result { get; set; }
}

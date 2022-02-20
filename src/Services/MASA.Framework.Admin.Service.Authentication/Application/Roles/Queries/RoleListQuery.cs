namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RoleListQuery(int PageIndex, int PageSize, string Name, int State) : Query<PaginatedItemResponse<RoleItemResponse>>
{
    public int State { get; set; } = State;

    public string Name { get; set; } = Name;

    public override PaginatedItemResponse<RoleItemResponse> Result { get; set; } = default!;
}

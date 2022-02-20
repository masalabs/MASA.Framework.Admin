namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public record PermissionListQuery(int PageIndex, int PageSize, string Name, int State) : Query<PaginatedItemResponse<PermissionItemResponse>>
{
    public int State { get; set; } = State;

    public string Name { get; set; } = Name;

    public override PaginatedItemResponse<PermissionItemResponse> Result { get; set; } = default!;
}

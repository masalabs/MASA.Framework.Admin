namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public record PermissionListQuery(int PageIndex, int PageSize, string Name, bool Enabled) : Query<PaginatedItemResponse<PermissionItemResponse>>
{
    public bool Enabled { get; set; } = Enabled;

    public string Name { get; set; } = Name;

    public override PaginatedItemResponse<PermissionItemResponse> Result { get; set; } = default!;
}

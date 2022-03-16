namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public record RoleListQuery(int PageIndex, int PageSize, string Name, bool Enabled) : Query<PaginatedItemResponse<RoleItemResponse>>
{
    public bool Enabled { get; set; } = Enabled;

    public string Name { get; set; } = Name;

    public override PaginatedItemResponse<RoleItemResponse> Result { get; set; } = default!;
}

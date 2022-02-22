namespace MASA.Framework.Admin.Configuration.Application.Menu.Queries;

public record ListMenuQuery(int PageIndex, int PageSize, string Name) : Query<PaginatedItemResponse<MenuItemResponse>>
{
    public override PaginatedItemResponse<MenuItemResponse> Result { get; set; } = default!;
}

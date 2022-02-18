namespace MASA.Framework.Admin.Configuration.Application.Menu.Queries;

public record AllQuery() : Query<List<MenuItemResponse>>
{
    public override List<MenuItemResponse> Result { get; set; } = default!;
}

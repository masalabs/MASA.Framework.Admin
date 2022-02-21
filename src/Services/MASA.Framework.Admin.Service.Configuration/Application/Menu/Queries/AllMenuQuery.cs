namespace MASA.Framework.Admin.Configuration.Application.Menu.Queries;

public record AllMenuQuery : Query<List<MenuItemResponse>>
{
    public override List<MenuItemResponse> Result { get; set; } = default!;
}

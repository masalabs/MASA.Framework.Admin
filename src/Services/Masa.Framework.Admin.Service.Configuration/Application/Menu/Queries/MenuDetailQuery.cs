namespace Masa.Framework.Admin.Configuration.Application.Menu.Queries;

public record MenuDetailQuery(Guid MenuId) : Query<MenuInfoResponse>
{
    public override MenuInfoResponse Result { get; set; } = default!;
}

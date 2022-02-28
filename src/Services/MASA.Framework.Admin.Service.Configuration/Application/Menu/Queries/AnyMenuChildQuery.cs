namespace Masa.Framework.Admin.Configuration.Application.Menu.Queries;

public record AnyMenuChildQuery(Guid menuId) : Query<bool>
{
    public override bool Result { get; set; }
}

namespace MASA.Framework.Admin.Configuration.Application.Menu.Queries;

public record AnyChildQuery(Guid menuId) : Query<bool>
{
    public override bool Result { get; set; }
}

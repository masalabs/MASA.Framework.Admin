namespace MASA.Framework.Admin.Service.Application.Orders.Queries
{
    public record OrderQuery : Query<List<Order1>>
    {
        public override List<Order1> Result { get; set; } = new();
    }
}
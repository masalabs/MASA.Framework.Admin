namespace Masa.Framework.Admin.Service.Application.Orders.Queries
{
    public record OrderQuery : Query<List<Order>>
    {
        public override List<Order> Result { get; set; } = new();
    }
}
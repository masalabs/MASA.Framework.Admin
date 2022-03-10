namespace Masa.Framework.Admin.Service.Application.Orders.Commands
{
    public record OrderCreateCommand : Command
    {
        public List<OrderItem> Items { get; set; } = new();
    }
}
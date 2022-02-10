namespace MASA.Framework.Admin.Service.Application.Orders
{
    public class OrderQueryHandler
    {
        readonly IOrderRepository _orderRepository;
        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [EventHandler]
        public void OrderListHandleAsync(OrderQuery query)
        {
            query.Result = _orderRepository.List().ToList();
        }
    }
}
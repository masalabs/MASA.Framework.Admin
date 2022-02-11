using MASA.Framework.Admin.Contracts.Order.Model;

namespace MASA.Framework.Admin.Caller
{
    public class OrderCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; } = "http://localhost:6083";

        public OrderCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(OrderCaller);
        }

        public async Task<List<Order>> List()
        {
            return await CallerProvider.GetAsync<List<Order>>($"order/query");
        }
    }
}
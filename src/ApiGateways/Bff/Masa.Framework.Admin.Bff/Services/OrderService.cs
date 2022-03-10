namespace Masa.Framework.Admin.Bff.Services
{
    public class OrderService : ServiceBase
    {
        readonly OrderCaller _orderCaller;

        public OrderService(IServiceCollection services, OrderCaller orderCaller) : base(services)
        {
            _orderCaller = orderCaller;
            App.MapGet("/api/v1/orders", List);
        }

        public async Task<IResult> List()
        {
            var data = await _orderCaller.List();
            return Results.Ok(data);
        }
    }
}
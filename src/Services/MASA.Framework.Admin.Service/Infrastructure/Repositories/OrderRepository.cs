namespace MASA.Framework.Admin.Service.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order1> List()
        {
            var data = Enumerable.Range(1, 5).Select(index =>
                  new Order1
                  {
                      CreateTime = DateTimeOffset.Now,
                      Id = index,
                      OrderNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                      Address = $"Address {index}"
                  }).ToList();
            return data;
        }
    }
}
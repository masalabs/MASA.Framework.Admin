namespace MASA.Framework.Admin.Services.BackgroundJobs.Application
{
    public class JobrQueryHandler
    {
        readonly IJobRepository _orderRepository;
        public JobrQueryHandler(IJobRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [EventHandler]
        public void OrderListHandleAsync(JobQuery query)
        {
            query.Result = _orderRepository.List().ToList();
        }
    }
}
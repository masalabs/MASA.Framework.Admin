namespace MASA.Framework.Admin.Services.BackgroundJobs.Application
{
    public class JobQueryHandler
    {
        readonly IJobRepository _orderRepository;
        public JobQueryHandler(IJobRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [EventHandler]
        public async Task OrderListHandleAsync(JobQuery query)
        {
            query.Result = await _orderRepository.List();
        }
    }
}
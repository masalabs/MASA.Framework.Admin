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
        public async Task JobListHandleAsync(JobQuery query)
        {
            query.Result = await _orderRepository.List();
        }


        [EventHandler]
        public async Task JobLogListHandleAsync(JobLogQuery query)
        {
            query.Result = await _orderRepository.LogList(query.jobId);
        }
    }
}
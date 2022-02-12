namespace MASA.Framework.Admin.Services.BackgroundJobs.Application
{
    public class JobQueryHandler
    {
        readonly IQueryRepository _queryRepository;
        public JobQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [EventHandler]
        public async Task JobListPageHandleAsync(JobQuery query)
        {
            query.Result = await _queryRepository.JobPageAsync(query.PageIndex, query.PageSize);
        }


        [EventHandler]
        public async Task JobLogPageHandleAsync(JobLogQuery query)
        {
            query.Result = await _queryRepository.JobLogPageAsync(query.jobId, query.PageIndex, query.PageSize);
        }
    }
}
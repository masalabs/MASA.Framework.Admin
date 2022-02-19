namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository, IBackgroundJobStore, IQueryRepository
    {
        private readonly JobDbContext _dbContext;

        public JobRepository(JobDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(Guid jobId)
        {
            var entity = await FindAsync(jobId);
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Job> FindAsync(Guid jobId)
        {
            return await _dbContext.Jobs.SingleAsync(x => x.Id == jobId);
        }

        public async Task<List<Job>> GetWaitingJobsAsync()
        {
            return await _dbContext.Jobs.Where(x => x.Enable == true).ToListAsync();
        }

        public async Task InsertAsync(Job job)
        {
            await _dbContext.Jobs.AddAsync(job);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InsertLogAsync(JobLog log)
        {
            await _dbContext.JobLogs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job jobInfo)
        {
            _dbContext.Jobs.Update(jobInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Job>> List()
        {
            return await _dbContext.Jobs.ToListAsync();
        }

        public async Task<List<JobLog>> LogList(Guid jobId)
        {
            return await _dbContext.JobLogs.Where(x => x.JobId == jobId).ToListAsync();
        }

        public async Task<PagingResult<JobViewModel>> JobPageAsync(int PageIndex, int PageSize)
        {
            var query = _dbContext.Jobs.OrderBy(r => r.CreateTime);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(s => new JobViewModel
            {
                Id = s.Id,
                Method = s.JobMethod,
                Args = s.JobArgs,
                Name = s.JobName,
                TryCount = s.TryCount,
                PeriodSeconds = s.PeriodSeconds,
                Enable = s.Enable,
                CreateTime = s.CreateTime.DateTime,
            }).ToListAsync();

            return new PagingResult<JobViewModel>(PageIndex, PageSize, totalCount, data);
        }

        public async Task<PagingResult<JobLogViewModel>> JobLogPageAsync(Guid jobId,int PageIndex, int PageSize)
        {
            var query = _dbContext.JobLogs.Where(x => x.JobId == jobId).OrderBy(r => r.CreateTime);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(s => new JobLogViewModel
            {
                JobId = s.JobId,
                JobName = s.JobName,
                JobResult = s.JobResult,
                CreateTime = s.CreateTime.DateTime,
            }).ToListAsync();

            return new PagingResult<JobLogViewModel>(PageIndex, PageSize, totalCount, data);
        }
    }
}
namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository, IBackgroundJobStore
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
            return await _dbContext.Jobs.Where(x => x.IsStop == false).ToListAsync();
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
    }
}
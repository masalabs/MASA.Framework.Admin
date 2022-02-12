namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public interface IBackgroundJobStore
{
    Task<Job> FindAsync(Guid jobId);

    Task<List<Job>> GetWaitingJobsAsync();

    Task UpdateAsync(Job jobInfo);

    Task InsertLogAsync(JobLog log);
}


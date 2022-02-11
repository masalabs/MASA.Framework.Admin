namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public interface IBackgroundJobStore
{
    Task<BackgroundJobInfo> FindAsync(Guid jobId);

    Task<List<BackgroundJobInfo>> GetWaitingJobsAsync();

    Task InsertAsync(BackgroundJobInfo job);

    Task DeleteAsync(Guid jobId);

    Task UpdateAsync(BackgroundJobInfo jobInfo);

    Task InsertLogAsync(BackgroundJobLog log);
}


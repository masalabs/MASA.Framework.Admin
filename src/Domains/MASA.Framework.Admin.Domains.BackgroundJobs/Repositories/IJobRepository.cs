namespace MASA.Framework.Admin.Domains.BackgroundJobs.Repositories
{
    public interface IJobRepository
    {
        Task<Job> FindAsync(Guid jobId);

        Task InsertAsync(Job job);

        Task UpdateAsync(Job job);

        Task DeleteAsync(Guid jobId);

        Task<List<Job>> List();

        Task<List<JobLog>> LogList(Guid jobId);


    }
}
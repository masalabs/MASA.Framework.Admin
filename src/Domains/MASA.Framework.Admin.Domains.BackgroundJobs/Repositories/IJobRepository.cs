namespace MASA.Framework.Admin.Domains.BackgroundJobs.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> List();

        Task<List<JobLog>> LogList(Guid jobId);
    }
}
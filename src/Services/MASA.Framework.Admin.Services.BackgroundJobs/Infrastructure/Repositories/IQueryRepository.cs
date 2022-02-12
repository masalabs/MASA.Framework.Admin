namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure.Repositories
{
    public interface IQueryRepository
    {
        Task<PagingResult<JobViewModel>> JobPageAsync(int PageIndex, int PageSize);

        Task<PagingResult<JobLogViewModel>> JobLogPageAsync(Guid jobId, int PageIndex, int PageSize);
    }
}

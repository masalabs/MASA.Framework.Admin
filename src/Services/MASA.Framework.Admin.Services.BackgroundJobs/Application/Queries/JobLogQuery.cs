namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Queries
{
    public record JobLogQuery(Guid jobId,int PageIndex, int PageSize) : Query<PagingResult<JobLogViewModel>>
    {
        public override PagingResult<JobLogViewModel> Result { get; set; } = new();
    }
}
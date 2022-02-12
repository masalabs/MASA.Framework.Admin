namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Queries
{
    public record JobQuery(int PageIndex, int PageSize) : Query<PagingResult<JobViewModel>>
    {
        public override PagingResult<JobViewModel> Result { get; set; } = new();
    }
}
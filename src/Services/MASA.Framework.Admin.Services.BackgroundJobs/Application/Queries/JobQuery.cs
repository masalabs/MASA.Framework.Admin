namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Queries
{
    public record JobLogQuery(Guid jobId) : Query<List<JobLog>>
    {
        public override List<JobLog> Result { get; set; } = new();
    }
}
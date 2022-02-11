namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Queries
{
    public record JobQuery : Query<List<Job>>
    {
        public override List<Job> Result { get; set; } = new();
    }
}
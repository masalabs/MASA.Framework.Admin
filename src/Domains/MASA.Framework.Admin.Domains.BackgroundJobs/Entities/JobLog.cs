namespace MASA.Framework.Admin.Domains.BackgroundJobs.Entities
{
    public class JobLog
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public string JobName { get; set; }

        public int JobResult { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTime.Now;
    }
}

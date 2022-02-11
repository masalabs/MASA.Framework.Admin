namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure.Entities
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Arags { get; set; }

        public TimeSpan Period { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;
    }
}
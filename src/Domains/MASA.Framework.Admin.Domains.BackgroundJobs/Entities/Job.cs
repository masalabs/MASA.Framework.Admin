namespace MASA.Framework.Admin.Domains.BackgroundJobs.Entities
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; }

        public string JobName { get; set; }

        public string JobMethod { get; set; }

        public string? JobArgs { get; set; }

        public short TryCount { get; set; }

        public bool Enable { get; set; }

        public int PeriodSeconds { get; set; }

        [NotMapped]
        public TimeSpan PeriodTime 
        {
            get { return TimeSpan.FromSeconds(PeriodSeconds); }
            set { PeriodSeconds = value.Seconds; }
        }

        public DateTime NextTryTime { get; set; }

        public DateTime? LastTryTime { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.Now;
    }
}
namespace MASA.Framework.Admin.Services.BackgroundJobs.ViewModel
{
    public class JobLogViewModel
    {
        public Guid JobId { get; set; }

        public string JobName { get; set; }

        public int JobResult { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

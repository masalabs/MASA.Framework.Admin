namespace MASA.Framework.Admin.Caller.Model.BackgroundJob.Request
{
    public class AddJobRequest
    {
        public string Name { get; set; }

        public string Method { get; set; }

        public string? Args { get; set; }

        public bool Enable { get; set; }

        public int PeriodSeconds { get; set; }
    }
}

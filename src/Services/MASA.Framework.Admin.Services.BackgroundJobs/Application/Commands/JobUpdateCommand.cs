namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Commands
{
    public record JobUpdateCommand: Command
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Method { get; set; }

        public string? Args { get; set; }

        public bool IsStop { get; set; }

        public int PeriodSeconds { get; set; }
    }
}

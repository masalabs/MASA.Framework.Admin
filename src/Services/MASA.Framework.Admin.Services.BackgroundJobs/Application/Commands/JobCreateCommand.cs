namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Commands
{
    public record JobCreateCommand : Command
    {
        public string Name { get; set; }

        public string Method { get; set; }

        public string? Args { get; set; }

        public bool IsStop { get; set; }

        public int PeriodSeconds { get; set; }
    }
}
namespace MASA.Framework.Admin.Services.BackgroundJobs.Application.Commands
{
    public record JobCreateCommand : Command
    {
        public List<JobLog> Items { get; set; } = new();
    }
}
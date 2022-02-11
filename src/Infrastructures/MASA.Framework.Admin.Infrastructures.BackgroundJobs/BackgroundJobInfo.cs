
namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public class BackgroundJobInfo
{
    public Guid Id { get; set; }

    public string JobName { get; set; }

    public string JobUri { get; set; }

    public string JobArgs { get; set; }

    public short TryCount { get; set; }

    public bool IsStop { get; set; }

    public TimeSpan PeriodTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime NextTryTime { get; set; }

    public DateTime? LastTryTime { get; set; }
}


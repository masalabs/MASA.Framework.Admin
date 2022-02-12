namespace MASA.Framework.Admin.Contracts.BackgroundJobs;

public interface IBackgroundWorker
{
    Task StartAsync(CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);
}

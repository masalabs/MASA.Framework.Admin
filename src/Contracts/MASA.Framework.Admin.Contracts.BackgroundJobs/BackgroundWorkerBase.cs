namespace MASA.Framework.Admin.Contracts.BackgroundJobs;

public abstract class BackgroundWorkerBase : IBackgroundWorker
{
    public IServiceProvider ServiceProvider { get; set; }

    protected CancellationTokenSource StoppingTokenSource { get; }
    protected CancellationToken StoppingToken { get; }

    public BackgroundWorkerBase()
    {
        StoppingTokenSource = new CancellationTokenSource();
        StoppingToken = StoppingTokenSource.Token;
    }

    public virtual Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public virtual Task StopAsync(CancellationToken cancellationToken = default)
    {
        StoppingTokenSource.Cancel();
        StoppingTokenSource.Dispose();
        return Task.CompletedTask;
    }
}


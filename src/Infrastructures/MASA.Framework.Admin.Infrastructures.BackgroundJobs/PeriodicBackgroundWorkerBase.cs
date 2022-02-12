namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
{
    private Timer _timer;

    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public int Period { get; set; } = 1000;

    protected PeriodicBackgroundWorkerBase(IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        _timer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
    }

    public override async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await base.StartAsync(cancellationToken);
        TimerStart();
    }

    public override async Task StopAsync(CancellationToken cancellationToken = default)
    {
        TimerStop();
        await base.StopAsync(cancellationToken);
    }

    private async Task DoWorkAsync()
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            try
            {
                await RunWorkAsync(new PeriodicBackgroundWorkerContext(scope.ServiceProvider));
            }
            catch (Exception ex)
            {
                // TODO: 异常处理
            }
        }
    }

    protected abstract Task RunWorkAsync(PeriodicBackgroundWorkerContext context);

    private void TimerStop()
    {
        lock(_timer)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

    private void TimerStart()
    {
        lock(_timer)
        {
            _timer.Change(Period, Timeout.Infinite);
        }
    }

    private void TimerCallBack(object? state)
    {
        TimerStop();
        _ = Elapsed();
    }

    private async Task Elapsed()
    {
        try
        {
            await DoWorkAsync();
        }
        catch (Exception ex)
        {
            // TODO:异常处理
        }
        finally
        {
            TimerStart();
        }
    }
}


namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public class PeriodicBackgroundWorkerContext
{
    public IServiceProvider ServiceProvider { get; }

    public PeriodicBackgroundWorkerContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}


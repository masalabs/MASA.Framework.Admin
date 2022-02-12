namespace MASA.Framework.Admin.Contracts.BackgroundJobs;

public class JobExecutionContext
{
    public IServiceProvider ServiceProvider { get; }

    public string JobMethod { get; }

    public string? JobArgs { get; }

    public JobExecutionContext(IServiceProvider serviceProvider, string jobMethod, string? jobArgs)
    {
        ServiceProvider = serviceProvider;
        JobMethod = jobMethod;
        JobArgs = jobArgs;
    }
}


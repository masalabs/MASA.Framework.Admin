namespace MASA.Framework.Admin.Contracts.BackgroundJobs;

public class JobExecutionContext
{
    public IServiceProvider ServiceProvider { get; }

    public string JobUri { get; }

    public object JobArgs { get; }

    public JobExecutionContext(IServiceProvider serviceProvider, string jobUri, object jobArgs)
    {
        ServiceProvider = serviceProvider;
        JobUri = jobUri;
        JobArgs = jobArgs;
    }
}


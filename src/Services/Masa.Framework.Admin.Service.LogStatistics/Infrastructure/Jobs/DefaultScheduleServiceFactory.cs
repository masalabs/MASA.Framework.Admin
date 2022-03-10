namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Jobs;

public class DefaultScheduleServiceFactory : IJobFactory
{
    protected IServiceProvider _serviceProvider;

    public DefaultScheduleServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        //Job类型
        Type jobType = bundle.JobDetail.JobType;

        //返回jobType对应类型的实例
        return _serviceProvider.GetService(jobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;

        disposable?.Dispose();
    }
}


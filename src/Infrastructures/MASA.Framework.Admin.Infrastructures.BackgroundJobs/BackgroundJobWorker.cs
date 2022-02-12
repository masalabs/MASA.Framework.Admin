namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public class BackgroundJobWorker : PeriodicBackgroundWorkerBase
{
    //private readonly List<IBackgroundJobExecuter> _executers;

    public BackgroundJobWorker(
        IServiceScopeFactory serviceScopeFactory)
        : base(serviceScopeFactory)
    {
        //_executers = new List<IBackgroundJobExecuter>();
    }

    protected override async Task RunWorkAsync(PeriodicBackgroundWorkerContext workContext)
    {
        // TODO : 加锁
        var store = workContext.ServiceProvider.GetRequiredService<IBackgroundJobStore>();

        var jobs = await store.GetWaitingJobsAsync();

        if (!jobs.Any()) return;

        var jobExecuter = workContext.ServiceProvider.GetRequiredService<IBackgroundJobExecuter>();

        foreach(var job in jobs)
        {
            if (job.NextTryTime > DateTime.Now) continue;

            job.TryCount++;
            job.LastTryTime = DateTime.Now;
            try
            {
                var context = new JobExecutionContext(workContext.ServiceProvider, job.JobMethod, job.JobArgs);

                try
                {
                    await jobExecuter.ExecuteAsync(context);

                    await store.InsertLogAsync(new JobLog
                    {
                        Id = Guid.NewGuid(),
                        JobId = job.Id,
                        JobName = job.JobName,
                        JobResult = (int)JobExecutionResult.Success,
                    });
                }
                catch (BackgroundJobExecutionException)
                {
                    await store.InsertLogAsync(new JobLog
                    {
                        Id = Guid.NewGuid(),
                        JobId = job.Id,
                        JobName = job.JobName,
                        JobResult = (int)JobExecutionResult.Failed,
                    });
                }
                finally
                {
                    job.NextTryTime = CalculateNextTryTime(job);
                }
            }
            catch (Exception ex)
            {
                // TODO: 处理异常
                job.Enable = false;
            }
            finally
            {
                await TryUpdateAsync(store, job);
            }
        }
    }

    private async Task TryUpdateAsync(IBackgroundJobStore store, Job job)
    {
        try
        {
            await store.UpdateAsync(job);
        }
        catch(Exception ex)
        {
            // TODO: 处理异常
        }
    }

    private DateTime CalculateNextTryTime(Job job)
    {
        return (job.LastTryTime ?? job.CreateTime.DateTime).Add(job.PeriodTime);
    }
}

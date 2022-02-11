namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs;

public class BackgroundJobWorker : PeriodicBackgroundWorkerBase
{
    public BackgroundJobWorker(
        IServiceScopeFactory serviceScopeFactory,
        Timer timer)
        : base(serviceScopeFactory,timer)
    {
        Period = 10;
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
            job.TryCount++;
            job.LastTryTime = DateTime.Now;
            try
            {
                var context = new JobExecutionContext(workContext.ServiceProvider, job.JobUri, job.JobArgs);

                try
                {
                    await jobExecuter.ExecuteAsync(context);

                    await store.InsertLogAsync(new BackgroundJobLog
                    {
                        Id = Guid.NewGuid(),
                        JobId = job.Id,
                        JobName = job.JobName,
                        ExecutionResult = JobExecutionResult.Success,
                    });
                }
                catch (BackgroundJobExecutionException)
                {
                    await store.InsertLogAsync(new BackgroundJobLog
                    {
                        Id = Guid.NewGuid(),
                        JobId = job.Id,
                        JobName = job.JobName,
                        ExecutionResult = JobExecutionResult.Failed,
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
                job.IsStop = true;
            }
            finally
            {
                await TryUpdateAsync(store, job);
            }
        }
    }

    private async Task TryUpdateAsync(IBackgroundJobStore store, BackgroundJobInfo job)
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

    private DateTime CalculateNextTryTime(BackgroundJobInfo job)
    {
        return (job.LastTryTime ?? job.CreationTime).Add(job.PeriodTime);
    }
}

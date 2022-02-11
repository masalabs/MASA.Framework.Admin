using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public class DefaultJobInterpreter : IJobInterpreter
    {
        public async Task ExecuteAsync(JobExecutionContext context)
        {
            var jobType = Type.GetType(context.JobUri)!;

            var job = context.ServiceProvider.GetService(jobType);

            if (job == null)
            {
                throw new ArgumentNullException();
            }

            var jobMethod = jobType.GetMethod(nameof(IBackgroundJob.Execute)) ??
                jobType.GetMethod(nameof(IAsyncBackgroundJob.ExecuteAsync));

            if (jobMethod == null)
            {
                throw new ArgumentNullException();
            }

            if (jobMethod.Name == nameof(IAsyncBackgroundJob.ExecuteAsync))
            {
                await (Task)jobMethod.Invoke(job, new[] { context.JobArgs })!;
            }
            else
            {
                jobMethod.Invoke(job, new[] { context.JobArgs });
            }
        }

        public Task OnExceptionNotifier(BackgroundJobExecutionException ex)
        {
            throw new NotImplementedException();
        }
    }
}

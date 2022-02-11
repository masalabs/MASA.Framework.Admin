using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public class BackgroundJobExecuter : IBackgroundJobExecuter
    {

        public virtual async Task ExecuteAsync(JobExecutionContext context)
        {
            try
            {
                var jobInterpreter = JobExecuterSelector(context);

                await jobInterpreter.ExecuteAsync(context);
            }
            catch (Exception ex)
            {
                //Logger.LogError("A background job execution is failed", ex.StackTrace);
                //await OnExceptionNotifier(new BackgroundJobExecutionException("A background job execution is failed", ex)
                //{
                //    JobType = context.JobUri,
                //    JobArgs = context.JobArgs
                //});
            }
        }

        public virtual IJobInterpreter JobExecuterSelector(JobExecutionContext context)
        {
            if(Regex.IsMatch(context.JobUri, "^(?:https?://)?[\\w]{1,}(?:\\.?[\\w]{1,})+[\\w-_/?&=#%:]*$"))
            {
                return context.ServiceProvider.GetService<HttpJobInterpreter>()!;
            }
            return context.ServiceProvider.GetService<DefaultJobInterpreter>()!;
        }
    }
}

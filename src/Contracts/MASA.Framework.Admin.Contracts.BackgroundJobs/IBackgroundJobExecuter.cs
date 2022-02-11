using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public interface IBackgroundJobExecuter
    {
        Task ExecuteAsync(JobExecutionContext context);

        //Task OnExceptionNotifier(BackgroundJobExecutionException ex);
    }
}

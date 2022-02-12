using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs
{
    public static class BackgroundJobWorkerExtension
    {
        public static void RegisterBackgroundJobWorker(this IServiceCollection services)
        {
            services.RegisterBackgroundJob();
            services.AddSingleton<IBackgroundWorker, BackgroundJobWorker>();
        }
    }
}

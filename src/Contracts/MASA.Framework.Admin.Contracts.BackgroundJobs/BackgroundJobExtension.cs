using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public static class BackgroundJobExtension
    {
        public static void RegisterBackgroundJob(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundJobExecuter, BackgroundJobExecuter>();
            services.AddSingleton<DefaultJobInterpreter>();
            services.AddSingleton<HttpJobInterpreter>();
        }
    }
}

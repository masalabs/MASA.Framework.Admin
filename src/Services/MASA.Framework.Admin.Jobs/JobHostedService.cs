using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MASA.Framework.Admin
{
    public class JobHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IServiceProvider _serviceProvider;
        private IScheduler _scheduler;

        public JobHostedService(ISchedulerFactory schedulerFactory, IServiceProvider serviceProvider)
        {
            _schedulerFactory = schedulerFactory;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start(cancellationToken);

            var assembly = Assembly.GetExecutingAssembly();
            var jobs = assembly.DefinedTypes.Where(r => r.IsClass && typeof(IJob).IsAssignableFrom(r));

            foreach (var job in jobs)
            {
                var jobAttribute = job.GetCustomAttribute<JobAttribute>();
                if (jobAttribute != null)
                {
                    var builder = TriggerBuilder.Create();
                    if (string.IsNullOrEmpty(jobAttribute.CornExpression))
                    {
                        builder.WithSimpleSchedule();
                    }
                    else
                    {
                        builder.WithCronSchedule(jobAttribute.CornExpression);
                    }
                    var trigger = builder
                                    .Build();

                    IDictionary<string, object> data = new Dictionary<string, object>
                                    {
                                        {nameof(IServiceProvider),_serviceProvider }
                                    };
                    var jobDetail = JobBuilder.Create(job.AsType())
                                    .WithIdentity(jobAttribute.Name, jobAttribute.Group)
                                    .UsingJobData(new JobDataMap(data))
                                    .Build();

                    await _scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}

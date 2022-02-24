using MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Commands;
using MASA.Framework.Admin.Service.LogStatistics.Infrastructure.Const;
using Quartz;

namespace MASA.Framework.Admin.Service.LogStatistics.Infrastructure.Jobs
{
    [Job(nameof(StatisticsJob), GroupNames.DEFAULT, "0 55 0/1 * * ?")]
    //[Job(nameof(StatisticsJob), GroupNames.DEFAULT, "* * * * * ?")]
    public class StatisticsJob : IJob
    {
        IEventBus _eventBus;

        //public StatisticsJob(IEventBus eventBus)
        //{
        //    _eventBus = eventBus;
        //}

        public async Task Execute(IJobExecutionContext context)
        {
            //todo change ctor ioc by IJobFactory
            if (_eventBus is null)
            {
                var serviceProvider = context.JobDetail.JobDataMap[nameof(IServiceProvider)] as IServiceProvider;
                var scope = serviceProvider.CreateScope();
                _eventBus = scope.ServiceProvider.GetService<IEventBus>();
            }

            await UpdateDayStatisticsAsync();
            await UpdateHourStatisticsAsync();
        }

        private async Task UpdateDayStatisticsAsync()
        {
            var date = DateTime.Now.Date;
            var startTime = date;
            var endTime = date.AddDays(1);

            var (pv, uv, ipCount) = await CountAsync(startTime, endTime);
            await _eventBus.PublishAsync(new UpdateDayStatisticsCommand(pv, uv, ipCount));
        }

        private async Task<(int pv, int uv, int ipCount)> CountAsync(DateTime startTime, DateTime endTime)
        {
            var query = new VisitStatisticsQuery(startTime, endTime);
            await _eventBus.PublishAsync(query);

            return (query.Result.PV, query.Result.UV, query.Result.IpCount);
        }

        private async Task UpdateHourStatisticsAsync()
        {
            var now = DateTime.Now;
            var time = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startTime = time;
            var endTime = time.AddHours(1);

            var (pv, uv, ipCount) = await CountAsync(startTime, endTime);
            await _eventBus.PublishAsync(new UpdateHourStatisticsCommand(pv, uv, ipCount));
        }
    }
}

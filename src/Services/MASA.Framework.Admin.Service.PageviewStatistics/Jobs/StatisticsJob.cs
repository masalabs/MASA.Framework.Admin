using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Contracts.PageviewStatistics;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace MASA.Framework.Admin.Service.PageviewStatistics
{
    [Job(nameof(StatisticsJob), GroupNames.Default, "0 55 0/1 * * ?")]
    public class StatisticsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var serviceProvider = context.JobDetail.JobDataMap[nameof(IServiceProvider)] as IServiceProvider;
            using (var scope = serviceProvider.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetService<PageviewStatisticsDbContext>())
                {
                    await UpdateDayStatisticsAsync(dbContext);
                    await UpdateHourStatisticsAsync(dbContext);
                }
            }
        }

        private static async Task UpdateDayStatisticsAsync(PageviewStatisticsDbContext dbContext)
        {
            var date = DateTime.Now.Date;

            var startTime = date;
            var endTime = date.AddDays(1);

            var (pv, uv, ipCount) = await CountAsync(dbContext, startTime, endTime);

            var dayStatistics = await dbContext.PageviewDayStatistics.SingleOrDefaultAsync(statistic => statistic.Date == date);
            if (dayStatistics == null)
            {
                dayStatistics = new PageviewDayStatistics
                {
                    PV = pv,
                    UV = uv,
                    IPCount = ipCount,
                    Date = date,
                };
                dbContext.Add(dayStatistics);
            }
            else
            {
                dayStatistics.PV = pv;
                dayStatistics.UV = uv;
                dayStatistics.IPCount = ipCount;
                dbContext.Update(dayStatistics);
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task<(int pv, int uv, int ipCount)> CountAsync(PageviewStatisticsDbContext dbContext, DateTime startTime, DateTime endTime)
        {
            var pv = await dbContext.OperationLogs
                .Where(log => log.Type == OperationLogType.VisitPage)
                .Where(log => log.CreateTime >= startTime && log.CreateTime <= endTime)
                .CountAsync();

            var uv = await dbContext.OperationLogs
                .Where(log => log.Type == OperationLogType.VisitPage)
                .Where(log => log.CreateTime >= startTime && log.CreateTime <= endTime)
                .GroupBy(log => log.UserId)
                .CountAsync();

            var ipCount = await dbContext.OperationLogs
                .Where(log => log.Type == OperationLogType.VisitPage)
                .Where(log => log.CreateTime >= startTime && log.CreateTime <= endTime)
                .GroupBy(log => log.ClientIP)
                .CountAsync();

            return (pv, uv, ipCount);
        }

        private async Task UpdateHourStatisticsAsync(PageviewStatisticsDbContext dbContext)
        {
            var now = DateTime.Now;
            var time = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);

            var startTime = time;
            var endTime = time.AddHours(1);

            var (pv, uv, ipCount) = await CountAsync(dbContext, startTime, endTime);

            var hourStatistics = await dbContext.PageviewHourStatistics.SingleOrDefaultAsync(statistic => statistic.Time == time);
            if (hourStatistics == null)
            {
                hourStatistics = new PageviewHourStatistics
                {
                    PV = pv,
                    UV = uv,
                    IPCount = ipCount,
                    Time = time,
                };
                dbContext.Add(hourStatistics);
            }
            else
            {
                hourStatistics.PV = pv;
                hourStatistics.UV = uv;
                hourStatistics.IPCount = ipCount;
                dbContext.Update(hourStatistics);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

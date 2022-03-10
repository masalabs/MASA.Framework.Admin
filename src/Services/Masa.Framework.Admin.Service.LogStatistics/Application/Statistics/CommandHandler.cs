namespace Masa.Framework.Admin.Service.LogStatistics.Application.Statistics
{
    public class CommandHandler
    {
        readonly IVisitStatisticsRecordRepository _visitStatisticsRecordRepository;

        public CommandHandler(IVisitStatisticsRecordRepository visitStatisticsRecordRepository)
        {
            _visitStatisticsRecordRepository = visitStatisticsRecordRepository;
        }

        [EventHandler]
        public async Task UpdateDayStatisticsAsync(UpdateDayStatisticsCommand updateDayStatisticsCommand)
        {
            var date = DateTime.Now.Date;
            var pv = updateDayStatisticsCommand.PV;
            var uv = updateDayStatisticsCommand.UV;
            var ipCount = updateDayStatisticsCommand.IpCount;

            var hourStatistics = await _visitStatisticsRecordRepository.FindAsync(statistic => statistic.DateTime == date);
            if (hourStatistics == null)
            {
                hourStatistics = new VisitStatisticsRecord(pv, uv, ipCount, VisitStatisticType.Day, date);
                await _visitStatisticsRecordRepository.AddAsync(hourStatistics);
            }
            else
            {
                hourStatistics.Update(pv, uv, ipCount);
                await _visitStatisticsRecordRepository.UpdateAsync(hourStatistics);
            }
        }

        [EventHandler]
        public async Task UpdateHourStatisticsAsync(UpdateHourStatisticsCommand updateHourStatisticsCommand)
        {
            var now = DateTime.Now;
            var time = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var pv = updateHourStatisticsCommand.PV;
            var uv = updateHourStatisticsCommand.UV;
            var ipCount = updateHourStatisticsCommand.IpCount;

            var hourStatistics = await _visitStatisticsRecordRepository.FindAsync(statistic => statistic.DateTime == time);
            if (hourStatistics == null)
            {
                hourStatistics = new VisitStatisticsRecord(pv, uv, ipCount, VisitStatisticType.Hour, time);
                await _visitStatisticsRecordRepository.AddAsync(hourStatistics);
            }
            else
            {
                hourStatistics.Update(pv, uv, ipCount);
                await _visitStatisticsRecordRepository.UpdateAsync(hourStatistics);
            }
        }
    }
}

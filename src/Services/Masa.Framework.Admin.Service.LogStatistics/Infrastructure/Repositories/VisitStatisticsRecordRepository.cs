namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Repositories;

public class VisitStatisticsRecordRepository : Repository<LogStatisticsDbContext, VisitStatisticsRecord>, IVisitStatisticsRecordRepository
{
    public VisitStatisticsRecordRepository(LogStatisticsDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}

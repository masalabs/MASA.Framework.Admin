namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Repositories;

public class OperationLogRepository : Repository<LogStatisticsDbContext, OperationLog>, IOperationLogRepository
{
    public OperationLogRepository(LogStatisticsDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}

using MASA.BuildingBlocks.Data.UoW;
using MASA.Contrib.DDD.Domain.Repository.EF;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Aggregates;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Repositories;

namespace MASA.Framework.Admin.Service.LogStatistics.Infrastructure.Repositories
{
    public class OperationLogRepository : Repository<LogStatisticsDbContext, OperationLog>, IOperationLogRepository
    {
        public OperationLogRepository(LogStatisticsDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}

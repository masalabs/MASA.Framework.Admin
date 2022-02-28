using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EF;
using Masa.Framework.Admin.Service.LogStatistics.Domain.Aggregates;
using Masa.Framework.Admin.Service.LogStatistics.Domain.Repositories;

namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Repositories
{
    public class OperationLogRepository : Repository<LogStatisticsDbContext, OperationLog>, IOperationLogRepository
    {
        public OperationLogRepository(LogStatisticsDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}

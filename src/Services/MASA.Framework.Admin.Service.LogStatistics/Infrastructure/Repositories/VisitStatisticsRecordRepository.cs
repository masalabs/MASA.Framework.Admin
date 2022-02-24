using MASA.BuildingBlocks.Data.UoW;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Aggregates;
using MASA.Framework.Admin.Service.LogStatistics.Domain.Repositories;

namespace MASA.Framework.Admin.Service.LogStatistics.Infrastructure.Repositories
{
    public class VisitStatisticsRecordRepository : Repository<LogStatisticsDbContext, VisitStatisticsRecord>, IVisitStatisticsRecordRepository
    {
        public VisitStatisticsRecordRepository(LogStatisticsDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}

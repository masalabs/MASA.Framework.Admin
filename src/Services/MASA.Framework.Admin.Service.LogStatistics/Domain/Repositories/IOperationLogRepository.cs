using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Framework.Admin.Service.LogStatistics.Domain.Aggregates;

namespace Masa.Framework.Admin.Service.LogStatistics.Domain.Repositories
{
    public interface IOperationLogRepository : IRepository<OperationLog>
    {
    }
}

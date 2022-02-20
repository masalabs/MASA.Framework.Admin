
using MASA.Framework.Admin.Contracts.Logging;

namespace MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories
{
    public interface IOperationLogRepository : IDisposable
    {
        IQueryable<OperationLog> GetAll();

        bool Create(OperationLog operationLog);
    }
}
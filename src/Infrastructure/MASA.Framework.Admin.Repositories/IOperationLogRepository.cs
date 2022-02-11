using MASA.Framework.Admin.Models;

namespace MASA.Framework.Admin.Repositories
{
    public interface IOperationLogRepository : IDisposable
    {
        IQueryable<OperationLog> GetAll();

        bool Create(OperationLog operationLog);
    }
}
using MASA.Framework.Admin.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Web.Services
{
    public interface IOperationLogService
    {
        Task LogAsync(string description, OperationLogType operationLogType);
    }
}

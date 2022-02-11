using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructure
{
    public interface IOperationLogService
    {
        Task LogAsync(string message);
    }
}

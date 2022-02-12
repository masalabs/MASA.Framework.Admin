using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructure
{
    public static class IOperationLogServiceExtensitions
    {
        public static async Task VisitPageAsync(this IOperationLogService operationLogService, string page)
        {
            await operationLogService.LogAsync($"访问了{page}页面!", OperationLogType.VisitPage);
        }
    }
}

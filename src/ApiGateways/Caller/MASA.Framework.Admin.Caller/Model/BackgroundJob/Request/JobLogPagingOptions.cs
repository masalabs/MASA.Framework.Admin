using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Model.BackgroundJob.Request
{
    public class JobLogPagingOptions:PagingOptions
    {
        public Guid JobId { get; set; }
    }
}

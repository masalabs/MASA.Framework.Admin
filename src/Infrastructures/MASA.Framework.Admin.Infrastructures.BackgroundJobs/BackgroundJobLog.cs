using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.BackgroundJobs
{
    public class BackgroundJobLog
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public string JobName { get; set; }

        public JobExecutionResult ExecutionResult { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}

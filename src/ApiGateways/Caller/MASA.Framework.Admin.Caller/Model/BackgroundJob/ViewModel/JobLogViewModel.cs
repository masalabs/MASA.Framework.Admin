using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Model.BackgroundJob.ViewModel
{
    public class JobLogViewModel
    {
        public Guid JobId { get; set; }

        public string JobName { get; set; }

        public int JobResult { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

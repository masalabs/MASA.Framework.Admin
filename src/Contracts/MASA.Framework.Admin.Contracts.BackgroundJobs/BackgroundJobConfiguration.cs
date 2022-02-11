using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public class BackgroundJobConfiguration
    {
        public string JobName { get; set; }

        public string JobUri { get; set; }

        public string JobArgs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Model.BackgroundJob.ViewModel
{
    public class JobViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Method { get; set; }

        public string? Args { get; set; }

        public short TryCount { get; set; }

        public bool Enable { get; set; }

        public int PeriodSeconds { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

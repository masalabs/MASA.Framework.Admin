using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.PageviewStatistics
{
    public class PageviewHourStatisticsQueryViewModel
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}

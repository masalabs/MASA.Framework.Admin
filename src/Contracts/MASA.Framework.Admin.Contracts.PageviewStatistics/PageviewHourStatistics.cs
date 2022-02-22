using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.PageviewStatistics
{
    [Table("pageview_hour_statistics")]
    public class PageviewHourStatistics
    {
        public int Id { get; set; }

        public int PV { get; set; }

        public int UV { get; set; }

        public int IPCount { get; set; }

        public DateTime Time { get; set; }
    }
}

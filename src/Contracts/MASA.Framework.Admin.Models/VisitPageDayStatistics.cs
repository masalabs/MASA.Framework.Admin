using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Models
{
    [Table("visit_page_day_statistics")]
    public class VisitPageDayStatistics
    {
        public int Id { get; set; }

        public int PV { get; set; }

        public int UV { get; set; }

        public int IPCount { get; set; }

        public DateTime Date { get; set; }
    }
}

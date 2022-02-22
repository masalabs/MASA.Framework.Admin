using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Logging
{
    public class OperationLogQueryViewModel
    {
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;

        [Range(1, int.MaxValue)]
        public int Limit { get; set; } = 10;

        public string Description { get; set; }
    }
}
